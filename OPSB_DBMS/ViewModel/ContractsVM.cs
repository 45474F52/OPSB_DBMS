using OPSB_DBMS.Model.DataBase;
using OPSB_DBMS.Model.DataBase.Commands;
using System.Collections.Generic;

namespace OPSB_DBMS.ViewModel
{
    internal class ContractsVM
    {
        public string Title => "Договоры";

        public List<Contract> Contracts { get; private set; }

        public ContractsVM()
        {
            Contracts = Select.GetContracts();
        }
    }
}