using System.Collections.Generic;

namespace Persistence.Entities
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
