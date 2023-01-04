using OPSB_DBMS.Model.DataBase.Commands;

namespace OPSB_DBMS.Model.DataBase
{
    public partial class Contract : ObservableType
    {
        public override int ObservableID => ID;

        /// <summary>
        /// Создаёт документ (<see cref="Contract"/>), инициализируя поля <see cref="Customer"/> и <see cref="Product"/>
        /// </summary>
        /// <param name="cID">Идентификатор клиента</param>
        /// <param name="pID">Идентификатор оборудования</param>
        public Contract(in int cID, in int pID) : base(typeof(Contract))
        {
            Customer = Select.GetClient(cID);
            Product = Select.GetProduct(pID);
        }

        public string ObservableAgreement
        {
            get => Agreement;
            set
            {
                Agreement = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public int ObservableCustomerID
        {
            get => CustomerID;
            set
            {
                CustomerID = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        public int ObservableProductID
        {
            get => ProductID;
            set
            {
                ProductID = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }
    }
}