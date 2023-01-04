using System;

namespace OPSB_DBMS.Core.DialogService
{
    /// <summary>
    /// Определяет диалоговое окно
    /// </summary>
    internal interface IModalWindow
    {
        /// <summary>
        /// Аналог <see langword="DialogResult"/>, обозначающий результат диалога
        /// </summary>
        bool? Result { get; set; }

        /// <summary>
        /// Событие, возникающее при закрытии диалогового окна
        /// </summary>
        event EventHandler OnClose;

        /// <summary>
        /// Открывает диалоговое окно
        /// </summary>
        void ShowView();

        /// <summary>
        /// Контекст данных окна, аналог <see langword="DataContext"/>
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Закрывает диалоговое окно
        /// </summary>
        void CloseView();
    }
}