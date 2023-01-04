using System;
using System.Runtime.Serialization;

namespace OPSB_DBMS.Core.Settings
{
    /// <summary>
    /// Универсальный класс, содержащий настройку любого типа
    /// </summary>
    [DataContract]
    [KnownType(typeof(StartupView))]
    internal class SettingsItem : ObservableObject
    {
        /// <summary>
        /// Имя свойства настройки
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Описание настройки
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Тип свойства <see cref="Value"/>
        /// </summary>
        [IgnoreDataMember]
        public Type SettingsType { get; set; }

        /// <summary>
        /// Представление свойства <see cref="SettingsType"/> в виде строки для возможности сериализации и десериализации
        /// </summary>
        [DataMember(Name = nameof(SettingsType))]
        public string SettingsTypeName
        {
            get => SettingsType?.AssemblyQualifiedName;
            set => SettingsType = value == null ? null : Type.GetType(value);
        }

        /// <summary>
        /// <see langword="true"/>, если свойство <see cref="Value"/> было изменено, иначе <see langword="false"/>
        /// </summary>
        [IgnoreDataMember]
        public bool IsDirty { get; set; }

        private dynamic _value;
        /// <summary>
        /// Значение свойства настройки
        /// </summary>
        [DataMember]
        public dynamic Value
        {
            get => _value;
            set
            {
                _value = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }
    }
}