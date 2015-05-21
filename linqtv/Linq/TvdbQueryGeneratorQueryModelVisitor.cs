using Remotion.Linq;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;

namespace Linqtv.Linq
{
    public class TvdbQueryGeneratorQueryModelVisitor : QueryModelVisitorBase
    {
        private IDictionary<string, string> AccumulatedParams { get; set; }

        public static IDictionary<string, string> GenerateUrlParameters(QueryModel queryModel)
        {
            var visitor = new TvdbQueryGeneratorQueryModelVisitor();
            visitor.VisitQueryModel(queryModel);

            return visitor.AccumulatedParams;
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            if (queryModel == null)
                throw new ArgumentNullException(nameof(queryModel));

            VisitBodyClauses(queryModel.BodyClauses, queryModel);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            if (whereClause == null)
                throw new ArgumentNullException(nameof(whereClause));

            AccumulatedParams = TvdbQueryExpressionVisitor.GenerateUrlParams(whereClause.Predicate);
            base.VisitWhereClause(whereClause, queryModel, index);
        }
    }
}