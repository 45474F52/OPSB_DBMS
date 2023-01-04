namespace OPSB_DBMS.Model.DataBase
{
    public partial class Customer : ObservableType
    {
        public override int ObservableID => ID;

        public string ObservableFullName
        {
            get => FullName ?? "Полное имя";
            set
            {
                FullName = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public string ObservablePhone
        {
            get => Phone ?? "Номер телефона";
            set
            {
                Phone = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public string ObservableEmail
        {
            get => Email ?? "Электронная почта";
            set
            {
                Email = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public string ObservableRequiredServices
        {
            get => Required_services ?? "Необходимые услуги";
            set
            {
                Required_services = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }
    }
}