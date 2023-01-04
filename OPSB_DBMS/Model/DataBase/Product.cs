using System.Collections.Generic;

namespace OPSB_DBMS.Model.DataBase
{
    public partial class Product
    {
        public Product() : base(typeof(Product))
        {
            Contracts = new HashSet<Contract>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}