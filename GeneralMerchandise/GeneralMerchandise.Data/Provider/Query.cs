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

        public abstract Query<TModel, TQueryType> Filter(params FilterCriterion<TQueryType>[] filterCritria);

        public abstract Query<TModel, TQueryType> Order(params OrderCriterion<TQueryType>[] orderCriteria);

        public abstract Query<TModel, TQueryType> Group(params GroupCriterion<TQueryType>[] groupCriteria);
        
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
