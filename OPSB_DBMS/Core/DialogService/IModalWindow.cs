using System;

namespace OPSB_DBMS.Core.DialogService
{
    /// <summary>
    /// Определяет диалоговое окно
    /// </summary>
    internal interface IModalWindow
    {
        bool? Result { get; set; }
        event EventHandler OnClose;
        void ShowView();
        object Data { get; set; }
        void CloseView();
    }
}