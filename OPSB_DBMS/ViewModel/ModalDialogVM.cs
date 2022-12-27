namespace OPSB_DBMS.ViewModel
{
    internal class ModalDialogVM
    {
        public ModalDialogVM(string capture, string message)
        {
            Capture = capture;
            Message = message;
        }

        public string Capture { get; private set; } = "Capture";
        public string Message { get; private set; } = "Message";
    }
}