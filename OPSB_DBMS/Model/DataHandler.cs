using OPSB_DBMS.Core;
using OPSB_DBMS.View;
using OPSB_DBMS.ViewModel;
using OPSB_DBMS.Core.DialogService;
using OPSB_DBMS.Model.DataBase.Commands;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OPSB_DBMS.Model
{
	/// <summary>
	/// Определяет базовые поля, методы и свойства классов, которые будут взаимодействовать с <see cref="ObservableType"/>
	/// </summary>
	/// <typeparam name="HandledData">Класс-наследник <see cref="ObservableType"/>, с которым будет производится взаимодействие</typeparam>
	internal abstract class DataHandler<HandledData> : ObservableObject where HandledData : ObservableType
    {
        /// <summary>
        /// Поле с именем <see cref="HandledData"/> как тип <see cref="NameOfData"/>
        /// </summary>
        private readonly NameOfData _dataName;

		/// <summary>
		/// Индексы объектов коллекции <see cref="Collection"/>, которые в неё были добавлены
		/// </summary>
        protected internal List<int> addedIndexes;

        /// <summary>
        /// Свойства ID объектов коллекции <see cref="Collection"/>, которые были удалены из неё
        /// </summary>
        protected internal List<int> removedIDs;

		/// <summary>
		/// Коллекция объектов <typeparamref name="HandledData"/>
		/// </summary>
        protected internal ObservableCollection<HandledData> Collection { get; set; }

        /// <summary>
        /// Отфильтрованная коллекция <see cref="Collection"/>&lt;<typeparamref name="HandledData"/>&gt;
        /// </summary>
        public abstract ObservableCollection<HandledData> FilteredCollection { get; }

		protected internal string _filter;
		public string Filter
		{
			get => _filter;
			set
			{
				_filter = value;
				OnPropertyChanged(nameof(FilteredCollection));
			}
		}

		protected internal int _selectedIndex;
		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				_selectedIndex = value;
				OnPropertyChanged();
			}
		}

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

		/// <summary>
		/// Инициализирует коллекцию <typeparamref name="HandledData"/>, а также команды и списки для взаимодействия с данными
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		protected internal DataHandler()
		{
            bool success = Enum.TryParse(typeof(HandledData).Name, out _dataName);

			if (success)
			{
				SetData();

				Collection.CollectionChanged += OnCollectionChanged;

				AddCommand = new RelayCommand(AddData);
				RemoveCommand = new RelayCommand(RemoveData);
				SaveCommand = new RelayCommand(SaveData);

				addedIndexes = new List<int>();
				removedIDs = new List<int>();
			}
			else
				throw new ArgumentException(
					"Переданный аргумент не удалось преобразовать в эквивалентный перечислимый объект NameOfData",
					typeof(HandledData).Name);
        }

		/// <summary>
		/// Метод, инициализирующий коллекцию <see cref="Collection"/>
		/// </summary>
		protected internal abstract void SetData();

		/// <summary>
		/// Метод, используемый в команде <see cref="AddCommand"/>
		/// </summary>
		/// <remarks>Предназначен для добавления объекта в коллекцию <see cref="Collection"/></remarks>
		protected internal abstract void AddData(object obj);

		/// <summary>
		/// Метод, используемый в команде <see cref="RemoveCommand"/>
		/// </summary>
		/// <remarks>Предназначен для удаления объекта из коллекции <see cref="Collection"/></remarks>
		protected internal virtual void RemoveData(object obj)
		{
			if (_selectedIndex >= 0)
			{
				if (addedIndexes.Contains(_selectedIndex))
					addedIndexes.Remove(_selectedIndex);
				else
					removedIDs.Add(Collection[_selectedIndex].ObservableID);

				Collection.RemoveAt(_selectedIndex);
			}
		}

		/// <summary>
		/// Метод, используемый в команде <see cref="SaveCommand"/>
		/// </summary>
		/// <remarks>Предназначен для сохранения изменений в БД</remarks>
		protected internal virtual void SaveData(object obj)
		{
            int inserted = default, deleted = default, updated = default;

            if (addedIndexes.Count > 0)
            {
                IEnumerable<HandledData> handledDatas = new Collection<HandledData>();

                foreach (int index in addedIndexes)
                {
                    handledDatas = handledDatas.Append(Collection[index]);
                    Collection.RemoveAt(index);
                }

                inserted = Insert.InsertHandledData(handledDatas);
                addedIndexes.Clear();
            }

            if (removedIDs.Count > 0)
            {
                deleted = Delete.DeleteHandledData(_dataName, removedIDs);
				removedIDs.Clear();
            }

            IEnumerable<HandledData> dirtyCollection = Collection.Where(u => u.IsDirty);
            if (dirtyCollection.Count() > 0)
            {
                updated = Update.UpdateHandledData(dirtyCollection);
                foreach (HandledData unit in dirtyCollection)
                    unit.ResetDirty();
            }

            App.ModalDialogService.ShowDialog(
				new ModalDialogView(),
                new ModalDialogVM("Сохранение изменений в БД прошло успешно!", $"Удалено: {deleted}\nИзменено: {updated}\nДобавлено: {inserted}"),
                DialogType.Notify);
        }

        /// <summary>
        /// Метод, срабатывающий при изменении коллекции <see cref="Collection"/>
        /// </summary>
        /// <remarks>По-умолчанию обрабатывает <see cref="NotifyCollectionChangedAction.Add"/> и <see cref="NotifyCollectionChangedAction.Remove"/></remarks>
        protected internal virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
            if (e.Action == NotifyCollectionChangedAction.Add)
                OnPropertyChanged(nameof(FilteredCollection));
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                OnPropertyChanged(nameof(FilteredCollection));
		}
	}
}