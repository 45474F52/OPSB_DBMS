using System;

namespace OPSB_DBMS.Core.DialogService
{
    /// <summary>
    /// Определяет реализацию вызова диалоговых окон
    /// </summary>
    internal interface IModalDialogService
    {
        /// <summary>
        /// Вызов показа диалогового окна с действием, срабатывающим при закрытии этого окна
        /// </summary>
        /// <typeparam name="TViewModel">Тип ViewModel окна</typeparam>
        /// <param name="view">Класс окна</param>
        /// <param name="viewModel">Класс ViewModel окна</param>
        /// <param name="onDialogClose">Метод, срабатываемый при закрытии диалогового окна</param>
        /// <param name="dialogType">Тип сообщения диалога</param>
        void ShowDialog<TViewModel>(IModalWindow view, TViewModel viewModel, Action<TViewModel> onDialogClose, DialogType dialogType);

        /// <summary>
        /// Вызов показа диалогового окна
        /// </summary>
        /// <typeparam name="TDialogVM">Тип ViewModel окна</typeparam>
        /// <param name="view">Класс окна</param>
        /// <param name="viewModel">Класс ViewModel окна</param>
        /// <param name="dialogType">Тип сообщения диалога</param>
        void ShowDialog<TDialogVM>(IModalWindow view, TDialogVM viewModel, DialogType dialogType);
    }

    /// <summary>
    /// Тип сообщения диалогового окна
    /// </summary>
    /// <remarks>В зависимости от типа меняется звуковое сопровождение появления диалогового окна</remarks>
    public enum DialogType
    {
        /// <summary>
        /// Тип отсутствует
        /// </summary>
        None,
        /// <summary>
        /// Обычное уведомление
        /// </summary>
        Notify,
        /// <summary>
        /// Уведомление об ошибке
        /// </summary>
        Error
    }
}