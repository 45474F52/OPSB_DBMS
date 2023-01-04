using OPSB_DBMS.Model;
using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Linq;
using System.Collections.ObjectModel;

namespace OPSB_DBMS.ViewModel
{
    internal class ClientsVM : DataHandler<Customer>
    {
        public string Title => "Клиенты";

        public override ObservableCollection<Customer> FilteredCollection => new ObservableCollection<Customer>(
            (from client in Collection
             where client.ObservableEmail.ToLower().Contains(_filter?.ToLower() ?? "") ||
             client.ObservablePhone.ToLower().Contains(_filter?.ToLower() ?? "")
             select client).ToList());

        public ClientsVM() : base() { }

        protected internal override void SetData() => Collection = new ObservableCollection<Customer>(Select.GetClients());

        protected internal override void AddData(object obj)
        {
            Collection.Add(new Customer());
            SelectedIndex = Collection.Count - 1;
            addedIndexes.Add(_selectedIndex);
        }
    }
}