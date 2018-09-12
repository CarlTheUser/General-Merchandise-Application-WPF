using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal abstract class ProductPersistence : IModelPersistence<ProductModel, long>
    {
        public abstract void Save(ProductModel model);

        public abstract void Edit(ProductModel model);

        public abstract void Delete(ProductModel model);

    }
}
