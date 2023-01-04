using System;
using System.ComponentModel;

namespace OPSB_DBMS.Core.Settings
{
    /// <summary>
    /// Определяет свойство как настройку для приложения и добавляет некоторые дополнительные свойства
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal sealed class SettingsAttribute : Attribute
    {
        public string Description { get; set; }

        [DefaultValue(true)]
        public bool Visible { get; set; }
    }
}