using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal class ProductModel : PersistibleModel<long>
    {
        public static readonly long DEFAULT_IDENTITY = 0;

        #region Static Factory Methods

        public static ProductModel New(string imageFilename, string name, string brand, string description, double unitPrice, int quantity = 0)
        {
            return new ProductModel(DEFAULT_IDENTITY, imageFilename, name, brand, description, unitPrice, quantity);
        }

        public static ProductModel Existing(long id, string imageFilename, string name, string brand, string description, double unitPrice, int quantity = 0)
        {
            return new ProductModel(id, imageFilename, name, brand, description, unitPrice, quantity);
        }

        #endregion

        #region Constructors

        private ProductModel() { }

        private ProductModel(long id, string imageFilename, string name, string brand, string description, double unitPrice, int quantity = 0)
        {
            Identity = id;
            ImageFilename = imageFilename;
            Name = name;
            Brand = brand;
            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        #endregion

        /// <summary>
        /// The Serial Number on a product barcode
        /// </summary>
        public override long Identity { get; set; }

        public string ImageFilename { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

    }
}
