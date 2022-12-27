using OPSB_DBMS.Core.DialogService;
using System;
using System.Windows;

namespace OPSB_DBMS.View
{
    public partial class ModalDialogView : IModalWindow
    {
        public ModalDialogView()
        {
            InitializeComponent();
        }

        public bool? Result { get; set; }
        public object Data { get; set; }

        public event EventHandler OnClose;

        public void CloseView()
        {
            Close();
            OnClose?.Invoke(this, EventArgs.Empty);
        }

        public void ShowView()
        {
            DataContext = Data;
            ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            DialogResult = Result;
        }
    }
}