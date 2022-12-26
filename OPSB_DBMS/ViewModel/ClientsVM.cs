using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Collections.Generic;

namespace OPSB_DBMS.ViewModel
{
    internal class ClientsVM
    {
        public string Title => "Клиенты";

        public List<Customer> Clients { get; private set; }

        public ClientsVM()
        {
            SetClients();
        }

        private void SetClients() => Clients = Select.GetClients();
    }
}