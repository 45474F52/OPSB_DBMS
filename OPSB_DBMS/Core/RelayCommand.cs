using System;
using System.Windows.Input;

namespace OPSB_DBMS.Core
{
    /// <summary>
    /// Базовый класс для определения команды
    /// </summary>
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Создаёт команду с действием <paramref name="execute"/> и свойством <paramref name="canExecute"/>
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнять команда</param>
        /// <param name="canExecute">Свойство, поределяющиее, может ли команда быть вызвана</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}