using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal interface IModelPersistence<TModel, TIdentity> 
        where TIdentity : IComparable<TIdentity>, IComparable 
        where TModel : PersistibleModel<TIdentity>
    {
        void Save(TModel model);

        void Edit(TModel model);

        void Delete(TModel model);

    }
}
