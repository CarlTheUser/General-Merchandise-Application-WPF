using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal class TagModel : IIdentifiable<int>
    {

        #region Factory Methods

        public static TagModel New(string name) { return new TagModel(name); }

        public static TagModel FromDB(int id, string name) { return new TagModel(id, name); }

        #endregion

        #region Constructor

        private TagModel(int id, string name) : this(name) { Identity = id; }

        private TagModel(string name) { Name = name; }

        #endregion

        public int Identity { get; set; }
        public string Name { get; set; }
    }
}
