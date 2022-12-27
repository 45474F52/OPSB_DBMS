using System;

namespace OPSB_DBMS.Core.DialogService
{
    /// <summary>
    /// Определяет реализацию вызова диалоговых окон
    /// </summary>
    internal interface IModalDialogService
    {
        void ShowDialog<TViewModel>(IModalWindow view, TViewModel viewModel, Action<TViewModel> onDialogClose, DialogType dialogType);
        void ShowDialog<TDialogVM>(IModalWindow view, TDialogVM viewModel, DialogType dialogType);
    }

    public enum DialogType
    {
        None,
        Notify,
        Error
    }
}