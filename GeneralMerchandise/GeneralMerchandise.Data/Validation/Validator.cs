using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Validation
{
    internal abstract class Validator<TModel> where TModel : class
    {
        protected Validator()
        {

        }

        protected Validator(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; set; } = null;

        public bool IsValid { get; private set; } = false;

        public string[] BrokenRules { get; private set; } = null;

        public bool Validate(TModel model)
        {
            Model = model;
            return Validate();
        }

        public bool Validate()
        {
            ValidationCriterion<TModel>[] criteria = GetValidationCriteria();

            ValidationCriterion<TModel>[] brokenRules = criteria.Where(x => !x.Validate(Model)).ToArray();

            BrokenRules = brokenRules.Select(x => x.ErrorMessage).ToArray();

            return IsValid = brokenRules.Length == 0;
        }

        public abstract ValidationCriterion<TModel>[] GetValidationCriteria();
    }
}
