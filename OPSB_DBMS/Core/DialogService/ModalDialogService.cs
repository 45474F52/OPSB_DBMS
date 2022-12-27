using System;
using System.Media;

namespace OPSB_DBMS.Core.DialogService
{
    /// <summary>
    /// Базовый класс, реализующий методы для отображения диалоговых окон
    /// </summary>
    internal class ModalDialogService : IModalDialogService
    {
        /// <summary>
        /// Вызов диалогового окна с действием при его закрытии
        /// </summary>
        /// <typeparam name="TViewModel">Тип ViewModel</typeparam>
        /// <param name="view">View</param>
        /// <param name="viewModel">ViewModel</param>
        /// <param name="onDialogClose">Действие при закрытии диалогового окна</param>
        /// <param name="dialogType">Тип диалога</param>
        public void ShowDialog<TViewModel>(IModalWindow view, TViewModel viewModel, Action<TViewModel> onDialogClose, DialogType dialogType = DialogType.None)
        {
            view.Data = viewModel;

            if (onDialogClose != null)
            {
                view.OnClose += (s, e) => onDialogClose(viewModel);
            }

            switch (dialogType)
            {
                case DialogType.None:
                    break;
                case DialogType.Notify:
                    SystemSounds.Asterisk.Play();
                    break;
                case DialogType.Error:
                    SystemSounds.Hand.Play();
                    break;
                default:
                    goto case DialogType.None;
            }

            view.ShowView();
        }

        /// <summary>
        /// Вызов диалогового окна
        /// </summary>
        /// <typeparam name="TDialogVM">Тип ViewModel</typeparam>
        /// <param name="view">View</param>
        /// <param name="viewModel">Model</param>
        /// <param name="dialogType">Тип диалога</param>
        public void ShowDialog<TDialogVM>(IModalWindow view, TDialogVM viewModel, DialogType dialogType = DialogType.None)
        {
            ShowDialog(view, viewModel, null, dialogType);
        }
    }
}