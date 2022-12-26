using OPSB_DBMS.Core;
using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Windows;

namespace OPSB_DBMS.ViewModel
{
    internal class ContractsVM : ObservableObject
    {
        private List<int> _addedIndexes;
        private List<int> _removedIDs;

        public string Title => "Договоры";

        private ObservableCollection<Contract> Contracts { get; set; }
        public ObservableCollection<Contract> FilteredContracts => new ObservableCollection<Contract>(
            (from contract in Contracts where contract.Customer.FullName.ToLower().Contains(_filter?.ToLower() ?? "") ||
             contract.Customer.Phone.ToLower().Contains(_filter?.ToLower() ?? "") select contract).ToList());

        private string _filter;
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged("FilteredProducts");
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand SaveChanges { get; private set; }

        public ContractsVM()
        {
            SetContracts();

            Contracts.CollectionChanged += OnCollectionChanged;

            AddCommand = new RelayCommand(obj =>
            {
                //Contracts.Add(new Contract());
                //SelectedIndex = Contracts.Count - 1;
                //_addedIndexes.Add(SelectedIndex);
            });

            RemoveCommand = new RelayCommand(obj =>
            {
                if (SelectedIndex >= 0)
                {
                    if (_addedIndexes.Contains(SelectedIndex))
                    {
                        _addedIndexes.Remove(SelectedIndex);
                    }
                    else
                    {
                        _removedIDs.Add(Contracts[SelectedIndex].ID);
                    }

                    Contracts.RemoveAt(SelectedIndex);
                }
            });

            SaveChanges = new RelayCommand(obj =>
            {
                //int inserted = default, deleted = default;

                //if (ThereAreChanges())
                //{
                //    if (_addedIndexes.Count > 0)
                //    {
                //        List<Contract> contracts = new List<Contract>();
                //        foreach (int index in _addedIndexes)
                //            contracts.Add(Contracts[index]);

                //        inserted = Insert.InsertProducts(contracts);
                //    }

                //    if (_removedIDs.Count > 0)
                //        deleted = Delete.DeleteProducts(_removedIDs);

                //    for (int i = 0; i < _addedIndexes.Count; i++)
                //        Contracts.RemoveAt(i);

                //    _addedIndexes.Clear();
                //}

                //MessageBox.Show($"Удалено: {deleted}\nДобавлено: {inserted}",
                //    "Сохранение изменений в базу данных прошло успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

                //inserted = default;
                //deleted = default;
            });

            _addedIndexes = new List<int>();
            _removedIDs = new List<int>();
        }

        private void SetContracts() => Contracts = new ObservableCollection<Contract>(Select.GetContracts());

        private bool ThereAreChanges() => _addedIndexes.Count > 0 || _removedIDs.Count > 0;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                OnPropertyChanged(nameof(FilteredContracts));
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                OnPropertyChanged(nameof(FilteredContracts));
        }
    }
}