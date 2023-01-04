using OPSB_DBMS.Model;
using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Linq;
using System.Collections.ObjectModel;

namespace OPSB_DBMS.ViewModel
{
    internal class ProductsVM : DataHandler<Product>
    {
        public string Title => "Оборудование";

        public override ObservableCollection<Product> FilteredCollection => new ObservableCollection<Product>(
            (from product in Collection
             where product.ObservableName.ToLower().Contains(_filter?.ToLower() ?? "") ||
                   product.ObservableCategory.ToLower().Contains(_filter?.ToLower() ?? "")
             select product).ToList());

        public ProductsVM() : base() { }

        protected internal override void SetData() => Collection = new ObservableCollection<Product>(Select.GetProducts());
        
        protected internal override void AddData(object obj)
        {
            Collection.Add(new Product());
            SelectedIndex = Collection.Count - 1;
            addedIndexes.Add(_selectedIndex);
        }
    }
}