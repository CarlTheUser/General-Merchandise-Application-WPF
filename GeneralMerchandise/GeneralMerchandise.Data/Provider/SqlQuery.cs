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
        
        
        public abstract class SqlFilterCriterion : FilterCriterion<string>
        {

            private static readonly string AND_CHAIN = "AND";

            private static readonly string OR_CHAIN = "OR";

            private string chainType; //Node

            public override string Evaluate()
            {
                string filterString = GetSQLClause();

                if (HasChainedCriteria) filterString += " " + chainType + " " + ChainedCriteria.Evaluate(); //Propagates the chain/node to get full filter string

                return filterString;
            }

            protected abstract string GetSQLClause();

            public SqlFilterCriterion ChainedCriteria { get; private set; }

            public bool HasChainedCriteria => ChainedCriteria != null;

            public abstract DbParameter[] GetParameters();

            public bool UsesParameter { get; protected set; } = false;

            public SqlFilterCriterion And(SqlFilterCriterion filterCriterion)
            {
                if(!HasChainedCriteria)
                {
                    ChainedCriteria = filterCriterion;
                    chainType = AND_CHAIN;
                }
                else ChainedCriteria.And(filterCriterion);

                return this;
            }

            public SqlFilterCriterion Or(SqlFilterCriterion filterCriterion)
            {
                if (!HasChainedCriteria)
                {
                    ChainedCriteria = filterCriterion;
                    chainType = OR_CHAIN;
                }
                else ChainedCriteria.Or(filterCriterion);

                return this;
            }

        }

        

       
    }
}
