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

            #region Static Constants

            private static readonly string AND_CHAIN = "AND";

            private static readonly string OR_CHAIN = "OR";

            #endregion

            private string chainType; //Node relationship (AND, OR)
            
            public SqlFilterCriterion NextCriteria { get; private set; } //Next Node in the filter series

            public bool HasNextCriteria => NextCriteria != null;

            public abstract DbParameter[] GetParameters(); //Support for Parameterized SQL querries. In array for BETWEEN scenarios (date between etc)

            public bool UsesParameter { get; protected set; } = false;

            protected abstract string GetSQLClause();

            public override string Evaluate() // Gets the SQL clauses and traverses the filter nodes in chain
            {
                string filterString = GetSQLClause();

                if (HasNextCriteria) filterString += " " + chainType + " " + NextCriteria.Evaluate(); //Propagates the chain/node to get full filter string

                return filterString;
            }

            public SqlFilterCriterion And(SqlFilterCriterion filterCriterion)
            {
                if(!HasNextCriteria)
                {
                    NextCriteria = filterCriterion;
                    chainType = AND_CHAIN;
                }
                else NextCriteria.And(filterCriterion); // Passes the filer to the next node until it replaces null at the end of the node.

                return this;
            }

            public SqlFilterCriterion Or(SqlFilterCriterion filterCriterion)
            {
                if (!HasNextCriteria)
                {
                    NextCriteria = filterCriterion;
                    chainType = OR_CHAIN;
                }
                else NextCriteria.Or(filterCriterion); // Passes the filer to the next node until it replaces null at the end of the node.

                return this;
            }

        }

        public class SqlOrderCriterion : OrderCriterion<string>
        {

            public enum OrderOptions
            {
                Ascending,
                Descensding
            }

            public string Column { get; }

            public OrderOptions Ordering { get; } 

            public SqlOrderCriterion NextCriterion { get; private set; }

            public bool HasNextCriteria => NextCriterion != null;

            internal SqlOrderCriterion(string column, OrderOptions ordering = OrderOptions.Ascending)
            {
                Column = column;
                Ordering = ordering;
            }

            public SqlOrderCriterion ChainWith(SqlOrderCriterion orderCriterion)
            {
                if (!HasNextCriteria) NextCriterion = orderCriterion;
                else NextCriterion.ChainWith(orderCriterion);
                return this;
            }

            public override string Evaluate()
            {
                string returnValue = GetSqlClause();

                if (HasNextCriteria) returnValue += ", " + NextCriterion.Evaluate();

                return returnValue;
            }

            private string GetSqlClause() { return string.Format("{0} {1}", Column, Ordering); }
        }

        public class SqlGroupCriterion : GroupCriterion<string>
        {

            public string Column { get; set; }

            public SqlGroupCriterion NextCriterion { get; private set; }

            public bool HasNextCriteria => NextCriterion != null;

            internal SqlGroupCriterion(string column) { Column = column; }

            public SqlGroupCriterion ChainWith(SqlGroupCriterion groupCriterion)
            {
                if (HasNextCriteria) NextCriterion.ChainWith(groupCriterion);
                else NextCriterion = groupCriterion;
                return this;
            }

            public override string Evaluate()
            {
                string returnValue = Column;

                if (HasNextCriteria) returnValue += ", " + NextCriterion.Evaluate();

                return returnValue;
            }

        }



    }
}
