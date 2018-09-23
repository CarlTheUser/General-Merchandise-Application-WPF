using GeneralMerchandise.Common.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Model
{
    public class AccountModel : PersistibleModel
    {

        #region Factory Methods

        public static AccountModel NewInstance() { return new AccountModel() { State = ModelState.New }; }

        public static AccountModel FromPersistentStorage(int id, string username, AccessType access, bool active)
        {
            return new AccountModel(id, username, access, active) { State = ModelState.Old };
        }

        #endregion

        #region Constructor

        private AccountModel() { }

        private AccountModel(int id, string username, AccessType access, bool active)
        {
            Id = id;
            Username = username;
            AccessType = access;
            IsActive = active;
        }

        #endregion

        #region Properties

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

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private AccessType accessType;

        public AccessType AccessType
        {
            get => accessType;
            set
            {
                accessType = value;
                OnPropertyChanged("AccessType");
            }
        }

        private bool active;

        public bool IsActive
        {
            get => active;
            set
            {
                active = value;
                OnPropertyChanged("IsActive");
            }
        }

        #endregion

        #region Implemented Behaviors

        private string username_backup = null;
        private AccessType? accessType_backup = null;
        private bool? active_backup = null;

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

        protected override void BackupProperties()
        {
            username_backup = Username;
            accessType_backup = AccessType;
            active_backup = IsActive;
        }

        protected override void RestoreProperties()
        {
            Username = username_backup;
            AccessType = accessType_backup.Value;
            IsActive = active_backup.Value;
        }

        protected override void ClearPropertyBackups()
        {
            username_backup = null;
            accessType_backup = null;
            active_backup = null;
        }

        #endregion
    }
}
