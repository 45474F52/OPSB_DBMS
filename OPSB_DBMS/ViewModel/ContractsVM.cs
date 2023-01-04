using OPSB_DBMS.Model;
using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;

namespace OPSB_DBMS.ViewModel
{
    internal class ContractsVM : DataHandler<Contract>
    {
        public string Title => "Договоры";

        public override ObservableCollection<Contract> FilteredCollection => new ObservableCollection<Contract>(
            (from contract in Collection where contract.Customer.FullName.ToLower().Contains(_filter?.ToLower() ?? "") ||
             contract.Customer.Phone.ToLower().Contains(_filter?.ToLower() ?? "") select contract).ToList());

        public ContractsVM() : base() { }

        protected internal override void SetData() => Collection = new ObservableCollection<Contract>(Select.GetContracts());

        protected internal override void AddData(object obj)
        {
            MessageBox.Show("Добавление договора не реализовано");
            //Collection.Add(new Contract());
            //SelectedIndex = Collection.Count - 1;
            //addedIndexes.Add(_selectedIndex);
        }
    }
}