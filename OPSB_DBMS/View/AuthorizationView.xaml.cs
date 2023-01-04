using System.Windows;

namespace OPSB_DBMS.View
{
    public partial class AuthorizationView : Window
    {
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            login.Focus();
        }
    }
}