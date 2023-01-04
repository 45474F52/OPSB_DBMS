using System.Reflection;
using System.Collections.Generic;

namespace OPSB_DBMS.Core.Settings
{
    /// <summary>
    /// Класс, содержащий в себе все виды настроек
    /// </summary>
    internal sealed class AppSettings
    {
        [Settings(Description = "Отображать дополнительные параметры?")]
        public bool AdditionalParametersVisibility { get; set; }

        [Settings(Description = "Какое окно открывать первым после авторизации?")]
        public StartupView StartupView { get; set; }

        /// <summary>
        /// Создаёт объект <see cref="AppSettings"/> и инициализирует настройки значениями по-умолчанию
        /// </summary>
        public AppSettings() { }

        /// <summary>
        /// Создаёт объект <see cref="AppSettings"/> и инициализирует все настройки, переданные в конструктор
        /// </summary>
        public AppSettings(in Dictionary<string, object> settings)
        {
            foreach (PropertyInfo property in typeof(AppSettings).GetProperties())
            {
                foreach (KeyValuePair<string, object> setting in settings)
                {
                    if (property.Name == setting.Key)
                        property.SetValue(this, setting.Value);
                }
            }
        }

        /// <summary>
        /// Метод, создающий список настроек, содержащихся в классе, в виде словаря <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <returns>Возвращает словарь, где <see langword="TKey"/> — название свойства (настройки), <see langword="TValue"/> — значение свойства</returns>
        public Dictionary<string, object> GetAsDictionary()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>();

            foreach (PropertyInfo setting in typeof(AppSettings).GetProperties())
                settings.Add(setting.Name, setting.GetValue(this));

            return settings;
        }
    }

    /// <summary>
    /// Список окон, которые можно выбрать в настройке отображения начального окна
    /// </summary>
    public enum StartupView : int
    {
        Оборудование,
        Клиенты,
        Договоры,
        Настройки
    }
}