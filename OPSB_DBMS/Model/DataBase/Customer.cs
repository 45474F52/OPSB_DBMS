using System.Collections.Generic;

namespace OPSB_DBMS.Model.DataBase
{
    public partial class Customer
    {
        public Customer() : base(typeof(Customer))
        {
            Assessments = new HashSet<Assessment>();
            Contracts = new HashSet<Contract>();
            Estimates = new HashSet<Estimate>();
        }
    
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Required_services { get; set; }
    
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
    }
}