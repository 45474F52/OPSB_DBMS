using OPSB_DBMS.Core;
using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OPSB_DBMS.ViewModel
{
    internal class ProductsVM : ObservableObject
    {
        private List<int> _addedIndexes;
        private List<int> _removedIDs;

        public string Title => "Оборудование";

        private ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> FilteredProducts => new ObservableCollection<Product>(
            (from product in Products where product.ObservableName.ToLower().Contains(_filter?.ToLower() ?? "") ||
             product.ObservableCategory.ToLower().Contains(_filter?.ToLower() ?? "") select product).ToList());

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

        public ProductsVM()
        {
            Products = new ObservableCollection<Product>(Select.GetProducts());

            Products.CollectionChanged += OnCollectionChanged;

            AddCommand = new RelayCommand(obj =>
            {
                Products.Add(new Product());
                SelectedIndex = Products.Count - 1;
                _addedIndexes.Add(SelectedIndex);
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
                        _removedIDs.Add(Products[SelectedIndex].ID);
                    }

                    Products.RemoveAt(SelectedIndex);
                }
            });

            SaveChanges = new RelayCommand(obj =>
            {
                int inserted = default, deleted = default, updated = default;

                if (ThereAreChanges())
                {
                    if (_addedIndexes.Count > 0)
                    {
                        List<Product> products = new List<Product>();
                        foreach (int index in _addedIndexes)
                            products.Add(Products[index]);

                        inserted = Insert.InsertProducts(products);
                    }

                    if (_removedIDs.Count > 0)
                        deleted = Delete.DeleteProducts(_removedIDs);

                    for (int i = 0; i < _addedIndexes.Count; i++)
                        Products.RemoveAt(i);

                    updated = Update.UpdateProducts(Products.Where(p => p.IsDirty).ToList());

                    _addedIndexes.Clear();

                    foreach (Product product in Products.Where(p => p.IsDirty))
                        product.ResetDirty();

                }

                MessageBox.Show($"Удалено: {deleted}\nИзменено: {updated}\nДобавлено: {inserted}",
                    "Сохранение изменений в базу данных прошло успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

                inserted = default; 
                deleted = default;
                updated = default;
            });

            _addedIndexes = new List<int>();
            _removedIDs = new List<int>();
        }

        private bool ThereAreChanges() =>
            _addedIndexes.Count > 0 || _removedIDs.Count > 0 || Products.Where(p => p.IsDirty).ToList().Count > 0;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                OnPropertyChanged(nameof(FilteredProducts));
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                OnPropertyChanged(nameof(FilteredProducts));
        }
    }
}