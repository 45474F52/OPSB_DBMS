using OPSB_DBMS.Core;
using OPSB_DBMS.Core.Settings;
using System.Linq;

namespace OPSB_DBMS.ViewModel
{
    internal class MainVM : ObservableObject
    {
        private readonly StartupView _startupView;

        private readonly ProductsVM _productsVM;
        private readonly ClientsVM _clientsVM;
        private readonly ContractsVM _contractsVM;
        private readonly SettingsVM _settingsVM;

        public RelayCommand ToProducts { get; private set; }
        public RelayCommand ToClients { get; private set; }
        public RelayCommand ToContracts { get; private set; }
        public RelayCommand ToSettings { get; private set; }

        private bool _productsCheck;
        public bool ProductsCheck
        {
            get => _productsCheck;
            set
            {
                _productsCheck = value;
                OnPropertyChanged();
            }
        }

        private bool _clientsCheck;
        public bool ClientsCheck
        {
            get => _clientsCheck;
            set
            {
                _clientsCheck = value;
                OnPropertyChanged();
            }
        }

        private bool _contractsCheck;
        public bool ContractsCheck
        {
            get => _contractsCheck;
            set
            {
                _contractsCheck = value;
                OnPropertyChanged();
            }
        }

        private bool _settingsCheck;
        public bool SettingsCheck
        {
            get => _settingsCheck;
            set
            {
                _settingsCheck = value;
                OnPropertyChanged();
            }
        }

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

        public MainVM()
		{
            _settingsVM = new SettingsVM();
            _startupView = (StartupView)_settingsVM.Settings.Single(s => s.SettingsType == typeof(StartupView)).Value;

            _productsVM = new ProductsVM();
            _clientsVM = new ClientsVM();
            _contractsVM = new ContractsVM();

            switch (_startupView)
            {
                case StartupView.Оборудование:
                    CurrentView = _productsVM;
                    ProductsCheck = true;
                    break;
                case StartupView.Клиенты:
                    CurrentView = _clientsVM;
                    ClientsCheck = true;
                    break;
                case StartupView.Договоры:
                    CurrentView = _contractsVM;
                    ContractsCheck = true;
                    break;
                case StartupView.Настройки:
                    CurrentView = _settingsVM;
                    SettingsCheck = true;
                    break;
                default:
                    throw new System.ArgumentException(nameof(_startupView));
            }

			ToProducts = new RelayCommand(obj =>
            {
                if (_currentView != _productsVM)
                    CurrentView = _productsVM;
            });

			ToClients = new RelayCommand(obj =>
            {
                if (_currentView != _clientsVM)
                    CurrentView = _clientsVM;
            });

            ToContracts = new RelayCommand(obj =>
            {
                if (_currentView != _contractsVM)
                    CurrentView = _contractsVM;
            });

            ToSettings = new RelayCommand(obj =>
            {
                if (_currentView != _settingsVM)
                    CurrentView = _settingsVM;
            });
        }
	}
}