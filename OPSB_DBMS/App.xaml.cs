using OPSB_DBMS.View;
using OPSB_DBMS.ViewModel;
using OPSB_DBMS.Core.DialogService;
using System.Windows;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;

namespace OPSB_DBMS
{
    public partial class App : Application
    {
        private static string _connectionString;
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        internal static string ConnectionString => _connectionString ?? (_connectionString = InitializeConnectionString());

        private static IModalDialogService _modalDialogService;
        /// <summary>
        /// Служба вызова диалогового окна
        /// </summary>
        internal static IModalDialogService ModalDialogService => _modalDialogService ?? (_modalDialogService = new ModalDialogService());

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new AuthorizationView() { DataContext = new AuthorizationVM() }; 
            MainWindow.Show();
            
            base.OnStartup(e);
        }

        /// <summary>
        /// Инициализирует строку подключения к БД (<see cref="ConnectionString"/>) значением из файла конфигурации
        /// </summary>
        /// <returns>Возвращает строку подключения к БД</returns>
        private static string InitializeConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OPSBEntities"].ConnectionString;
            if (connectionString.ToLower().StartsWith("metadata="))
                connectionString = new EntityConnectionStringBuilder(connectionString).ProviderConnectionString;

            return connectionString;
        }

        /// <summary>
        /// Меняет значение строки подключения <see cref="ConnectionString"/>
        /// </summary>
        /// <param name="connectionString">Новая строка подключения</param>
        public static void SetConnectionString(string connectionString) => _connectionString = connectionString;
    }
}