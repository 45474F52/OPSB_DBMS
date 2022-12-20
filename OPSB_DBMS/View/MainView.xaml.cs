using System.Windows;

namespace OPSB_DBMS.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Close();
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WrapButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            WrapButton.Visibility = Visibility.Collapsed;
            UnwrapButton.Visibility = Visibility.Visible;
        }

        private void UnwrapButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            WrapButton.Visibility = Visibility.Visible;
            UnwrapButton.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                WrapButton.Visibility = Visibility.Visible;
                UnwrapButton.Visibility = Visibility.Collapsed;
            }
            else if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                WrapButton.Visibility = Visibility.Collapsed;
                UnwrapButton.Visibility = Visibility.Visible;
            }
        }
    }
}