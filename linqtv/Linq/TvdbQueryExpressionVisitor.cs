﻿using Remotion.Linq.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace Linqtv.Linq
{
    internal class TvdbQueryExpressionVisitor : ThrowingExpressionVisitor
    {
        private ImmutableDictionary<string, string> AccumulatedParams { get; set; } = ImmutableDictionary<string, string>.Empty;

        public static IDictionary<string, string> GenerateUrlParams(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var visitor = new TvdbQueryExpressionVisitor();
            visitor.Visit(expression);
            return visitor.AccumulatedParams;
        }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            throw new NotImplementedException(StringUtilities.Invariant($"{visitMethod} visit method is not implemented in {nameof(TvdbQueryExpressionVisitor)}"));
        }

        protected override Expression VisitMember(MemberExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if ((expression.Member.Name != "SeriesName") || (expression.Member.DeclaringType != typeof(Model.Show)))
                throw new NotImplementedException("Only .Name property of Show is supported");

            return expression;
        }

        protected override Expression VisitConstant(ConstantExpression expression) => expression;

        protected override Expression VisitBinary(BinaryExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.NodeType != ExpressionType.Equal)
                throw new NotSupportedException("Only equal binary expressions supported");

            if (expression.Left.NodeType != ExpressionType.MemberAccess)
                throw new NotSupportedException(StringUtilities.Invariant($"Left side of a binary expression in .Where must be a {ExpressionType.MemberAccess}"));

            if (expression.Right.NodeType != ExpressionType.Constant)
                throw new NotSupportedException(StringUtilities.Invariant($"Right side of a binary expression in .Where must be a {nameof(ExpressionType.Constant)}"));

            var leftMember = Visit(expression.Left) as MemberExpression;
            var rightConstant = Visit(expression.Right) as ConstantExpression;

            AccumulatedParams = AccumulatedParams.SetItem(leftMember.Member.Name, rightConstant.Value as string);

            return expression;
        }
    }
}