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
            public virtual DbParameter[] GetParameters() { return null; } //Support for Parameterized SQL querries. In array for multiple parameters scenario like BETWEEN (date between etc)

            public bool UsesParameter { get; protected set; } = false;

            protected internal abstract string GetSQLClause();

            public override string Evaluate() 
            {
                return GetSQLClause();
            }

            public SqlFilterCriterion And(SqlFilterCriterion filterCriterion)
            {
                return new CompoundSqlFilterCriterion(this, filterCriterion, CompoundSqlFilterCriterion.LINK_AND);
            }

            public SqlFilterCriterion Or(SqlFilterCriterion filterCriterion)
            {
                return new CompoundSqlFilterCriterion(this, filterCriterion, CompoundSqlFilterCriterion.LINK_OR);
            }

            public SqlFilterCriterion CreateBrackets() => new ParenthesizedSqlFilterCriterion(this);

        }

        public class CompoundSqlFilterCriterion : SqlFilterCriterion
        {
            public static readonly string LINK_AND = "AND";

            public static readonly string LINK_OR = "OR";

            private SqlFilterCriterion LeftFilter { get; }

            private SqlFilterCriterion RightFilter { get; }

            string Link { get; }

            public CompoundSqlFilterCriterion(SqlFilterCriterion left, SqlFilterCriterion right, string link)
            {
                LeftFilter = left;
                RightFilter = right;
                Link = link;
                UsesParameter = LeftFilter.UsesParameter || RightFilter.UsesParameter;
            }

            protected internal override string GetSQLClause()
            {
                return $"{LeftFilter.GetSQLClause()} {Link} {RightFilter.GetSQLClause()}";
            }

            public override DbParameter[] GetParameters()
            {
                DbParameter[] leftParams;
                DbParameter[] rightParams;

                if (LeftFilter.UsesParameter && RightFilter.UsesParameter)
                {
                    leftParams = LeftFilter.GetParameters();
                    rightParams = RightFilter.GetParameters();

                    return leftParams.Concat(rightParams).ToArray();
                }
                else if (LeftFilter.UsesParameter) return LeftFilter.GetParameters();
                else if (RightFilter.UsesParameter) return RightFilter.GetParameters();

                return null;
            }
        }

        public class ParenthesizedSqlFilterCriterion : SqlFilterCriterion
        {

            private SqlFilterCriterion GroupedCriterion { get; }

            public ParenthesizedSqlFilterCriterion(SqlFilterCriterion groupedCriterion)
            {
                GroupedCriterion = groupedCriterion;
                UsesParameter = GroupedCriterion.UsesParameter;
            }

            public override DbParameter[] GetParameters()
            {
                return GroupedCriterion.GetParameters();
            }

            protected internal override string GetSQLClause()
            {
                return $"({GroupedCriterion.GetSQLClause()})";
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
