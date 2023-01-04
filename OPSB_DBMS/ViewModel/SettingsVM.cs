using OPSB_DBMS.View;
using OPSB_DBMS.Core;
using OPSB_DBMS.Core.Settings;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;

namespace OPSB_DBMS.ViewModel
{
    internal class SettingsVM : ObservableObject
    {
        /// <summary>
        /// Путь к файлу с настройками
        /// </summary>
        private readonly string _path;
        private readonly AppSettings _appSettings;

        private bool _maySaveSettings = true;

        public string Title => "Настройки";

        private ObservableCollection<SettingsItem> _settings;
        public ObservableCollection<SettingsItem> Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveSettings { get; private set; }

        /// <summary>
        /// Инициализирует путь к файлу настроек, объект <see cref="AppSettings"/> и команду сохранения настроек
        /// </summary>
        public SettingsVM()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
            _appSettings = DeserializeSettings();

            SaveSettings = new RelayCommand(SerializeSettings, canExecute => _maySaveSettings);
        }

        /// <summary>
        /// Создаёт список настроек (<see cref="SettingsItem"/>) при помощи рефлексии, считывая метаданные класса <see cref="AppSettings"/>
        /// </summary>
        /// <param name="settings">Словарь настроек, где TKey — название свойства, TValue — значение свойства</param>
        /// <returns>Возвращает список настроек, где <see langword="TKey"/> — название свойства, <see langword="TValue"/> — значение свойства</returns>
        private ObservableCollection<SettingsItem> GetAvaliableSettings(in Dictionary<string, object> settings)
        {
            Collection<SettingsItem> settingsView = new Collection<SettingsItem>();
            
            foreach (PropertyInfo property in typeof(AppSettings).GetProperties())
            {
                SettingsItem settingsItem = new SettingsItem();

                foreach (CustomAttributeData attributeData in property.GetCustomAttributesData())
                {
                    foreach (CustomAttributeNamedArgument item in attributeData.NamedArguments)
                    {
                        if (item.MemberInfo.Name == "Description")
                            settingsItem.Description = item.TypedValue.Value.ToString();
                    }

                    settingsItem.Name = property.Name;
                    settingsItem.Value = settings[property.Name];
                    settingsItem.SettingsType = settingsItem.Value.GetType();
                    settingsItem.IsDirty = false;

                    settingsView.Add(settingsItem);
                }
            }

            return new ObservableCollection<SettingsItem>(settingsView.Distinct().ToList());
        }

        /// <summary>
        /// Читает содержимое файла настроек и возвращает значение. А также инициализирует список настроек <see cref="Settings"/>
        /// </summary>
        /// <returns>Возвращает проинициализированный объект <see cref="AppSettings"/>, если удалось прочитать содержимое файла настроек,
        /// <br/> иначе возвращает обычный объект <see cref="AppSettings"/></returns>
        private AppSettings DeserializeSettings()
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<SettingsItem>));

            if (File.Exists(_path))
            {
                using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Settings = (ObservableCollection<SettingsItem>)jsonSerializer.ReadObject(stream);
                }

                Dictionary<string, object> settings = new Dictionary<string, object>();

                foreach (SettingsItem setting in _settings)
                    settings.Add(setting.Name, setting.Value);

                return new AppSettings(settings);
            }
            else
            {
                AppSettings appSettings = new AppSettings();
                Settings = GetAvaliableSettings(appSettings.GetAsDictionary());
                return appSettings;
            }
        }

        /// <summary>
        /// Сохраняет список настроект <see cref="Settings"/> в файл
        /// </summary>
        private void SerializeSettings(object obj)
        {
            _maySaveSettings = false;

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<SettingsItem>));

            using (FileStream stream =
                new FileStream(_path, File.Exists(_path) ? FileMode.Create : FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                jsonSerializer.WriteObject(stream, _settings);
            }

            App.ModalDialogService.ShowDialog(
                new ModalDialogView(),
                new ModalDialogVM("Настройки сохранены!", "Сохранение настроек прошло успешно"),
                Core.DialogService.DialogType.Notify);

            _maySaveSettings = true;
        }
    }
}