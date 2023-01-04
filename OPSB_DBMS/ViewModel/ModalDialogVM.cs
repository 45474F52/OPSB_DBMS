namespace OPSB_DBMS.ViewModel
{
    internal class ModalDialogVM
    {
        public string Capture { get; private set; } = "Capture";
        public string Message { get; private set; } = "Message";

        /// <summary>
        /// Инициализирует сообщение и заголовок диалогового окна
        /// </summary>
        /// <param name="capture"></param>
        /// <param name="message"></param>
        public ModalDialogVM(string capture, string message)
        {
            Capture = capture;
            Message = message;
        }
    }
}