using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Validation
{
    class ValidationCriterion <T>
    {
        public string ErrorMessage { get; private set; }

        public Func<T, bool> ValidationRule{ get; private set; }

        public ValidationCriterion(Func<T, bool> validationRule, string errorMessage)
        {
            ValidationRule = validationRule;
            ErrorMessage = errorMessage;
        }

        public bool Validate(T model)
        {
            return ValidationRule != null ? ValidationRule.Invoke(model) : true;
        }
    }
}
