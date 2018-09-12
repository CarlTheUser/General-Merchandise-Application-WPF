using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal abstract class UserPersistence : IModelPersistence<UserModel, int>
    {
        public abstract void Save(UserModel model);

        public abstract void Edit(UserModel model);

        public abstract void Delete(UserModel model);

    }
}
