using Remotion.Linq;
using Remotion.Linq.Clauses;
using System.Collections.Generic;

namespace linqtv.Linq
{
    public class TvdbQueryGeneratorQueryModelVisitor : QueryModelVisitorBase
    {
        private IDictionary<string, string> AccumulatedParams { get; set; }

        public static IDictionary<string, string> GenerateUrlParams(QueryModel queryModel)
        {
            var visitor = new TvdbQueryGeneratorQueryModelVisitor();
            visitor.VisitQueryModel(queryModel);

            return visitor.AccumulatedParams;
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            VisitBodyClauses(queryModel.BodyClauses, queryModel);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            AccumulatedParams = TvdbQueryExpressionVisitor.GenerateUrlParams(whereClause.Predicate);
            base.VisitWhereClause(whereClause, queryModel, index);
        }
    }
}