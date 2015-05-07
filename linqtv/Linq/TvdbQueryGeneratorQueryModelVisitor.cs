using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remotion.Linq.Clauses;
using Remotion.Linq;
using System.Collections.ObjectModel;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;

namespace linqtv.Linq
{
    public class TvdbQueryGeneratorQueryModelVisitor : QueryModelVisitorBase
    {
        public static IDictionary<string, string> GenerateUrlParams(QueryModel queryModel)
        {
            var visitor = new TvdbQueryGeneratorQueryModelVisitor();
            visitor.VisitQueryModel(queryModel);

            return visitor.GatherParameters();
        }

        private IDictionary<string, string> GatherParameters()
        {
            return default(IDictionary<string, string>);
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            queryModel.SelectClause.Accept(this, queryModel);
            queryModel.MainFromClause.Accept(this, queryModel);
            VisitBodyClauses(queryModel.BodyClauses, queryModel);
            VisitResultOperators(queryModel.ResultOperators, queryModel);
        }
        public override void VisitAdditionalFromClause(AdditionalFromClause fromClause, QueryModel queryModel, int index)
        {

        }
        public override void VisitGroupJoinClause(GroupJoinClause groupJoinClause, QueryModel queryModel, int index)
        {

        }
        public override void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, GroupJoinClause groupJoinClause)
        {

        }
        public override void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, int index)
        {

        }
        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            base.VisitMainFromClause(fromClause, queryModel);
        }
        public override void VisitOrderByClause(OrderByClause orderByClause, QueryModel queryModel, int index)
        {

        }
        public override void VisitOrdering(Ordering ordering, QueryModel queryModel, OrderByClause orderByClause, int index)
        {

        }
        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {

        }
        public override void VisitSelectClause(SelectClause selectClause, QueryModel queryModel)
        {
            TvdbQueryExpressionVisitor.GenerateUrlParams(selectClause.Selector);
            base.VisitSelectClause(selectClause, queryModel);
        }
        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            TvdbQueryExpressionVisitor.GenerateUrlParams(whereClause.Predicate);
            base.VisitWhereClause(whereClause, queryModel, index);
        }
        protected override void VisitBodyClauses(ObservableCollection<IBodyClause> bodyClauses, QueryModel queryModel)
        {
            base.VisitBodyClauses(bodyClauses, queryModel);
        }
        protected override void VisitOrderings(ObservableCollection<Ordering> orderings, QueryModel queryModel, OrderByClause orderByClause)
        {

        }
        protected override void VisitResultOperators(ObservableCollection<ResultOperatorBase> resultOperators, QueryModel queryModel)
        {

        }


    }
}
