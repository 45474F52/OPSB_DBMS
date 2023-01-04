using OPSB_DBMS.Core;
using System;

namespace OPSB_DBMS.Model
{
    /// <summary>
    /// Класс, который должны наследовать те классы, объекты которых сохраняются в БД
    /// </summary>
    public abstract class ObservableType : ObservableObject
    {
        /// <summary>
        /// Отображает значение поля ID у класса-наследника
        /// </summary>
        public abstract int ObservableID { get; }

        /// <summary>
        /// <see langword="true"/>, если произошли изменения значений свойств у класса-наследника
        /// </summary>
        public bool IsDirty { get; protected internal set; }

        /// <summary>
        /// Сбрасывает поле <see cref="IsDirty"/>
        /// </summary>
        public void ResetDirty() => IsDirty = false;

        /// <summary>
        /// Поле с именем класса-наследника как тип <see cref="NameOfData"/>
        /// </summary>
        internal readonly NameOfData nameOfData;

        /// <summary>
        /// Определяет поле объекта <see cref="nameOfData"/> по переданному типу
        /// </summary>
        /// <param name="childType">Тип объекта, который наследует этот класс</param>
        /// <exception cref="ArgumentException"></exception>
        public ObservableType(Type childType)
        {
            bool success = Enum.TryParse(childType.Name, out nameOfData);
            if (!success)
                throw new ArgumentException(
                    "Переданный аргумент не удалось преобразовать в эквивалентный перечислимый объект NameOfData",
                    childType.Name);
        }
    }
}