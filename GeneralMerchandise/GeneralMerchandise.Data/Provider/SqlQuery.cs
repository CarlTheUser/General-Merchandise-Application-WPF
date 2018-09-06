using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal abstract class SqlQuery<TModel> : Query<TModel, string>
    {
        public override abstract IEnumerable<TModel> Execute();

        //public override abstract Query<TModel, string> Filter(FilterCriterion<string> filterCritria);

        //public abstract override Query<TModel, string> Group(params GroupCriterion<string>[] groupCriteria);

        //public override abstract Query<TModel, string> Order(params OrderCriterion<string>[] orderCriteria);

        public abstract class SqlFilterCriterion : FilterCriterion<string>
        {

            private static readonly string AND_CHAIN = "AND";

            private static readonly string OR_CHAIN = "OR";

            protected string chainType;

            public abstract override string Evaluate();

            public SqlFilterCriterion ChainedCriteria { get; private set; }

            public bool HasChainedCriteria => ChainedCriteria != null;

            public abstract DbParameter[] GetParameters();

            public bool UsesParameter { get; protected set; }

            public SqlFilterCriterion And(SqlFilterCriterion filterCriterion)
            {
                ChainedCriteria = filterCriterion;
                chainType = AND_CHAIN;
                return this;
            }

            public SqlFilterCriterion Or(SqlFilterCriterion filterCriterion)
            {
                ChainedCriteria = filterCriterion;
                chainType = OR_CHAIN;
                return this;
            }

        }

        

       
    }
}
