using System;
using System.Collections.Generic;

namespace WSSale.Models
{
    public partial class Concept
    {
        public long Id { get; set; }
        public long IdSale { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Import { get; set; }
        public int IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; } = null!;
        public virtual Sale IdSaleNavigation { get; set; } = null!;
    }
}
