using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OPSB_DBMS.Core
{
    /// <summary>
    /// Базовый класс, реализующий оповещения при изменении свойства
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод оповещения об изменении свойства <paramref name="propertyName"/>
        /// </summary>
        /// <param name="propertyName">Название изменённого свойства</param>
        private protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}