using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMerchandise.Common;

namespace GeneralMerchandise.Data.Validation
{
    internal class UserValidator : Validator<UserModel>
    {
        public UserValidator() { }

        public UserValidator(UserModel model) : base(model) { }

        private bool HasFirstname(UserModel user) => user.Firstname.HasValue();

        private bool HasLastname(UserModel user) => user.Lastname.HasValue();

        private bool IsFirstnameAlphabetic(UserModel user) => user.Firstname.IsAlphabetic();

        private bool IsLastnameAlphabetic(UserModel user) => user.Lastname.IsAlphabetic();

        private bool IsMiddlenameEmptyOrAlphabetic(UserModel user)
        {
            string middlename = user.Middlename.Trim();

            return middlename.Length == 0 || middlename.IsAlphabetic();
        }

        private bool IsGenderValid(UserModel user) => user.Gender != Common.Type.Gender.Unknown;

        private bool IsBirthdatePast(UserModel user) => DateTime.Now > user.BirthDate;

        private bool IsContactNumberValidLength(UserModel user) => user.ContactNumber.IsInRange(7, 11);

        private bool IsContactNumberNumeric(UserModel user) => user.ContactNumber.IsNumeric();

        public override ValidationCriterion<UserModel>[] GetValidationCriteria()
        {
            return new ValidationCriterion<UserModel>[]
            {
                new ValidationCriterion<UserModel>(HasFirstname, "Firstname is required."),
                new ValidationCriterion<UserModel>(IsFirstnameAlphabetic, "Firstname must be alphabetic."),
                new ValidationCriterion<UserModel>(IsMiddlenameEmptyOrAlphabetic, "Middlename should be alphabetic or empty."),
                new ValidationCriterion<UserModel>(HasLastname, "Lastname is required."),
                new ValidationCriterion<UserModel>(IsLastnameAlphabetic, "Lastname must be alphabetic."),
                new ValidationCriterion<UserModel>(IsGenderValid, "Proper gender must be specified."),
                new ValidationCriterion<UserModel>(IsBirthdatePast, "Birthdate must be from the past."),
                new ValidationCriterion<UserModel>(IsContactNumberValidLength, "Contact number must be valid."),
                new ValidationCriterion<UserModel>(IsContactNumberNumeric, "Contact number must be numeric.")

            };
        }

    }
}
