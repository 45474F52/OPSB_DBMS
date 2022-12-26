using OPSB_DBMS.Core;
using OPSB_DBMS.Model.Security;
using OPSB_DBMS.View;
using System.Data.SqlClient;
using System.Security;
using System.Windows;

namespace OPSB_DBMS.ViewModel
{
    internal class AuthorizationVM : ObservableObject
    {
        public string Title => "Авторизация";

		private string _login;
		public string Login
		{
			get => _login;
			set
			{
				_login = value;
				OnPropertyChanged();
			}
		}

		private SecureString _password;
		public SecureString Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged();
			}
		}

		public RelayCommand AuthorizationCommand { get; private set; }

		public AuthorizationVM()
		{
			AuthorizationCommand = new RelayCommand(Authorization);
		}

		private void Authorization(object obj)
		{
			bool authValue;

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(App.ConnectionString);

            authValue = _login == builder.UserID && _password.ToUnsecuredString() == builder.Password;

			if (authValue)
			{
				Window currentWindow = Application.Current.MainWindow;
                Application.Current.MainWindow = new MainView() { DataContext = new MainVM() };
				currentWindow.Close();
				Application.Current.MainWindow.Show();
			}
			else
			{
				MessageBox.Show("Неверные логин или пароль!");
			}
		}
	}
}