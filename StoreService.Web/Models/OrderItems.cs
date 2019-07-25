using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Web.Models
{
    public partial class OrderItems
    {
        public int OrderItemsId { get; set; }
        public int? OrdersId { get; set; }
        public string Description { get; set; }

    }
}
