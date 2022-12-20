using OPSB_DBMS.Core;

namespace OPSB_DBMS.ViewModel
{
    internal class MainVM : ObservableObject
    {
		private ProductsVM _productsVM;
		private ClientsVM _clientsVM;
		private ContractsVM _contractsVM;

		public MainVM()
		{
			CurrentView = _productsVM ?? (_productsVM = new ProductsVM());
		}

		private RelayCommand _toProducts;
		public RelayCommand ToProducts => _toProducts ??
			(_toProducts = new RelayCommand(obj => CurrentView = _productsVM));

        private RelayCommand _toClients;
        public RelayCommand ToClients => _toClients ??
			(_toClients = new RelayCommand(obj => CurrentView = _clientsVM ?? 
							(_clientsVM = new ClientsVM())));

		private RelayCommand _toContracts;
		public RelayCommand ToContracts => _toContracts ??
			(_toContracts = new RelayCommand(obj => CurrentView = _contractsVM ??
							(_contractsVM = new ContractsVM())));

        private object _currentView;
		public object CurrentView
		{
			get => _currentView;
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}
	}
}