using OPSB_DBMS.View;
using OPSB_DBMS.ViewModel;
using System.Windows;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;

namespace OPSB_DBMS
{
    public partial class App : Application
    {
        private static string _connectionString;
        internal static string ConnectionString => _connectionString ?? (_connectionString = InitializeConnectionString());

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainView() { DataContext = new MainVM() };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private static string InitializeConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OPSBEntities"].ConnectionString;
            if (connectionString.ToLower().StartsWith("metadata="))
                connectionString = new EntityConnectionStringBuilder(connectionString).ProviderConnectionString;

            return connectionString;
        }
    }
}