namespace OPSB_DBMS.Model.DataBase
{
    public partial class Product : ObservableType
    {
        public override int ObservableID => ID;

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
    }
}