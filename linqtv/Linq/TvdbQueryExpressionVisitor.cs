using Remotion.Linq.Parsing;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace linqtv.Linq
{
    public class TvdbQueryExpressionVisitor : ThrowingExpressionTreeVisitor
    {
        private ImmutableDictionary<string, string> AccumulatedParams { get; set; } = ImmutableDictionary<string, string>.Empty;

        public static IDictionary<string, string> GenerateUrlParams(Expression expression)
        {
            var visitor = new TvdbQueryExpressionVisitor();
            visitor.VisitExpression(expression);
            return visitor.AccumulatedParams;
        }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            throw new NotImplementedException($"{visitMethod} visit method is not implemented in {nameof(TvdbQueryExpressionVisitor)}");
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            if ((expression.Member.Name != "Name") || (expression.Member.DeclaringType != typeof(Model.Show)))
                throw new NotImplementedException("Only .Name property of Show is supported");

            return expression;
        }

        protected override Expression VisitConstantExpression(ConstantExpression expression) => expression;

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            if (expression.NodeType != ExpressionType.Equal)
                throw new NotSupportedException("Only equal binary expressions supported");

            if (expression.Left.NodeType != ExpressionType.MemberAccess)
                throw new NotSupportedException($"Left side of a binary expression in .Where must be a {ExpressionType.MemberAccess}");

            if (expression.Right.NodeType != ExpressionType.Constant)
                throw new NotSupportedException($"Right side of a binary expression in .Where must be a {nameof(ExpressionType.Constant)}");

            var leftMember = VisitExpression(expression.Left) as MemberExpression;
            var rightConstant = VisitExpression(expression.Right) as ConstantExpression;

            AccumulatedParams = AccumulatedParams.SetItem(leftMember.Member.Name, rightConstant.Value as string);


            return expression;
        }
    }
}
