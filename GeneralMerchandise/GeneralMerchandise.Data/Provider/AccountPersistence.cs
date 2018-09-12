using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal abstract class AccountPersistence : IModelPersistence<AccountModel, int>
    {
        public abstract void Save(AccountModel model);

        public abstract void Edit(AccountModel model);

        public abstract void Delete(AccountModel model);

    }
}
