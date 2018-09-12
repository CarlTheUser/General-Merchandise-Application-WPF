using GeneralMerchandise.Common.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Model
{
    class UserModel : PersistibleModel
    {

        #region Factory Methods

        public static UserModel NewInstance() { return new UserModel() { State = ModelState.New }; }

        public static UserModel FromPersistentStorage(
            int id,
            string imageFileLocation,
            string firstname,
            string middlename,
            string lastname,
            Gender gender,
            DateTime birthdate,
            string contactNumber,
            string email,
            string address
            )
        {
            return new UserModel(id, imageFileLocation, firstname, middlename, lastname, gender, birthdate, contactNumber, email, address) { State = ModelState.Old };
        }

        #endregion

        #region Constructor

        private UserModel() { }

        private UserModel(
            int id, 
            string imageFileLocation, 
            string firstname, 
            string middlename, 
            string lastname, 
            Gender gender, 
            DateTime birthdate, 
            string contactNumber, 
            string email, 
            string address)
        {
            Id = id;
            ImageFileLocation = imageFileLocation;
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
            Gender = gender;
            BirthDate = birthdate;
            ContactNumber = contactNumber;
            Email = email;
            Address = address;
        }

        #endregion

        #region Public Properties

        private int id;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string imageFileLocation;

        public string ImageFileLocation
        {
            get => imageFileLocation;
            set
            {
                imageFileLocation = value;
                OnPropertyChanged("ImageFileLocation");
            }
        }

        private string firstname;

        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        private string middlename;

        public string Middlename
        {
            get => middlename;
            set
            {
                firstname = value;
                OnPropertyChanged("Middlename");
            }
        }

        private string lastname;

        public string Lastname
        {
            get => lastname;
            set
            {
                firstname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public string Fullname
        {
            get
            {
                if (Middlename != null && Middlename.Trim().Length > 0)
                {
                    return $"{Firstname} {Middlename} {Lastname}";
                }
                else return $"{Firstname} {Lastname}";
            }
        }

        private Gender gender;

        public Gender Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private DateTime birthdate;

        public DateTime BirthDate
        {
            get => birthdate;
            set
            {
                birthdate = value;
                OnPropertyChanged("Birthdate");
            }
        }

        private string contactNumber;

        public string ContactNumber
        {
            get => contactNumber;
            set
            {
                contactNumber = value;
                OnPropertyChanged("ContactNumber");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        #endregion

        #region Implemented Behaviors

        protected override void SaveMethod()
        {
            throw new NotImplementedException();
        }

        protected override void EditMethod()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteMethod()
        {
            throw new NotImplementedException();
        }

        private string imagefilelocation_backup = null;
        private string firstname_backup = null;
        private string middlename_backup = null;
        private string lastname_backup = null;
        private Gender gender_backup = Gender.Unknown;
        private DateTime birthdate_backup = DateTime.MinValue;
        private string contactnumber_backup = null;
        private string email_backup = null;
        private string address_backup = null;


        protected override void BackupProperties()
        {
            imagefilelocation_backup = ImageFileLocation;
            firstname_backup = Firstname;
            middlename_backup = Middlename;
            lastname_backup = Lastname;
            gender_backup = Gender;
            birthdate_backup = BirthDate;
            contactnumber_backup = ContactNumber;
            email_backup = Email;
            address_backup = Address;
        }

        protected override void RestoreProperties()
        {
            ImageFileLocation = imagefilelocation_backup;
            Firstname = firstname_backup;
            Middlename = middlename_backup;
            Lastname = lastname_backup;
            Gender = gender_backup;
            BirthDate = birthdate_backup;
            ContactNumber = contactnumber_backup;
            Email = email_backup;
            Address = address_backup;
        }

        protected override void ClearPropertyBackups()
        {
            imagefilelocation_backup = null;
            firstname_backup = null;
            middlename_backup = null;
            lastname_backup = null;
            gender_backup = Gender.Unknown;
            birthdate_backup = DateTime.MinValue;
            contactnumber_backup = null;
            email_backup = null;
            address_backup = null;
        }

        #endregion

    }
}
