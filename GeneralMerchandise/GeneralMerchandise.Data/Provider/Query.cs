using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal abstract class Query<TModel, TQueryType>
    {

        public abstract IEnumerable<TModel> Execute();

        public FilterCriterion<TQueryType> Filter { get; set; }

        public OrderCriterion<TQueryType> Ordering { get; set; }

        public GroupCriterion<TQueryType> Grouping { get; set; }
        
        public abstract class FilterCriterion<T>
        {
            public abstract T Evaluate();
        }

        public abstract class OrderCriterion<T>
        {
            public abstract T Evaluate();
        }

        public abstract class GroupCriterion<T>
        {
            public abstract T Evaluate();
        }
        
    }
}
