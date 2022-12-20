using OPSB_DBMS.Model.DataBase.Commands;

namespace OPSB_DBMS.Model.DataBase
{
    public partial class Contract
    {
        /// <summary>
        /// Создаёт документ (<see cref="Contract"/>), инициализируя поля <see cref="Customer"/> и <see cref="Product"/>
        /// </summary>
        /// <param name="cID">Идентификатор клиента</param>
        /// <param name="pID">Идентификатор оборудования</param>
        public Contract(in int cID, in int pID)
        {
            Customer = Select.GetClient(cID);
            Product = Select.GetProduct(pID);
        }
    }
}