using Remotion.Linq.Parsing;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Remotion.Linq.Clauses.Expressions;
using System.Diagnostics;
using Remotion.Linq.Clauses;

namespace linqtv.Linq
{
    public class TvdbQueryExpressionVisitor : ThrowingExpressionTreeVisitor
    {
        public static IDictionary<string, string> GenerateUrlParams(Expression expression)
        {
            var visitor = new TvdbQueryExpressionVisitor();
            visitor.VisitExpression(expression);
            return default(IDictionary<string, string>);
        }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitQuerySourceReferenceExpression(QuerySourceReferenceExpression expression)
        {
            Debug.WriteLine($"VisitQuerySourceReferenceExpression({expression.ReferencedQuerySource.ItemName} of {expression.ReferencedQuerySource.ItemType} )");
            return expression;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            Debug.WriteLine($"VisitMemberExpression({expression.Member.Name} of {expression.Member.MemberType} )");
            VisitExpression(expression.Expression);

            return expression;
        }

        protected override Expression VisitConstantExpression(ConstantExpression expression)
        {
            Debug.WriteLine($"Constant({expression.Type}:{expression.Value} )");

            return expression;
        }

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            Debug.WriteLine($"VisitBinaryExpression(${expression.NodeType}, ");

            VisitExpression(expression.Left);
            VisitExpression(expression.Right);

            Debug.WriteLine(")");

            return expression;
        }


    }
}
