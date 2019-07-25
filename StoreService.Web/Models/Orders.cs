using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Web.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrdersId { get; set; }
        public string CustomerName { get; set; }
        public DateTime DeliveryDate { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
