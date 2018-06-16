using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal abstract class SqlQuery<TModel> : Query<TModel, string>
    {
        public override abstract IEnumerable<TModel> Execute();

        public override abstract Query<TModel, string> Filter(params FilterCriterion<string>[] filterCritria);

        public abstract override Query<TModel, string> Group(params GroupCriterion<string>[] groupCriteria);

        public override abstract Query<TModel, string> Order(params OrderCriterion<string>[] orderCriteria);

        public class SqlFilterCriterion : FilterCriterion<string>
        {

            protected string Operator { get; set; }

            public override string Evaluate()
            {
                return string.Empty;
            }

            public SqlFilterCriterion And(SqlFilterCriterion filterCriterion)
            {
                return null;
            }

            public SqlFilterCriterion Or(SqlFilterCriterion filterCriterion)
            {
                return null;
            }

        }

        internal class CompoundSqlFilterCriterion : SqlFilterCriterion
        {
            public CompoundSqlFilterCriterion()
            {

            }
        }
    }
}
