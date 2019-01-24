using System;
using System.Collections.Generic;

namespace Persistence.Entities
{
    public class Order
    {
        public Order()
        {
            Products = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> Products { get; set; }
    }
}
