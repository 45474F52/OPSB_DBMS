using OPSB_DBMS.Core;

namespace OPSB_DBMS.Model.DataBase
{
    /// <summary>
    /// Предоставляет свойства, оповещающие интерфейс об их изменении
    /// </summary>
    public partial class Product : ObservableObject
    {
        /// <summary>
        /// Были ли свойства <see cref="Product"/> изменены
        /// </summary>
        /// <remarks><see langword="true"/>, если свойства были изменены, иначе <see langword="false"/></remarks>
        public bool IsDirty { get; private set; }

        public string ObservableName
		{
			get => Name ?? "Название";
			set
			{
				Name = value;
                IsDirty = true;
				OnPropertyChanged();
			}
		}

		public string ObservableDescription
		{
			get => Description ?? "Описание";
			set
			{
				Description = value;
                IsDirty = true;
                OnPropertyChanged();
			}
		}

        public string ObservableCategory
        {
            get => Category ?? "Категория";
            set
            {
                Category = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public string ObservableBrand
        {
            get => Brand ?? "Брэнд";
            set
            {
                Brand = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public string ObservableManufacturer
        {
            get => Manufacturer ?? "Поставщик";
            set
            {
                Manufacturer = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public int ObservableQuantity
        {
            get => Quantity;
            set
            {
                Quantity = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public decimal ObservablePrice
        {
            get => Price;
            set
            {
                Price = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сбрасывает значение <see cref="IsDirty"/>
        /// </summary>
        public void ResetDirty() => IsDirty = false;
    }
}