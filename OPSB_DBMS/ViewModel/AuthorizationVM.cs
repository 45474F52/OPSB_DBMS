using OPSB_DBMS.View;
using OPSB_DBMS.Core;
using OPSB_DBMS.Model.Security;
using OPSB_DBMS.Core.DialogService;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Windows;
using System.Security;
using System.Data.SqlClient;

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
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(App.ConnectionString)
            {
                UserID = _login ?? string.Empty,
                Password = _password.ToUnsecuredString()
            };

			App.SetConnectionString(builder.ConnectionString);

            SqlException ex = Select.ConnectionCheck();
			if (ex != null)
			{
				App.ModalDialogService.ShowDialog(
					new ModalDialogView(),
					new ModalDialogVM(ex.Message, "Неверные логин или пароль!\nПроверьте правильность заполненных полей"),
					DialogType.Error);
            }
			else
			{
				Password.Dispose();
				_password.Dispose();

				Window authorizationWindow = Application.Current.MainWindow;
				Application.Current.MainWindow = new MainView();
				authorizationWindow.Close();
				Application.Current.MainWindow.Show();
			}
		}
	}
}