using System;
using System.Collections.Generic;

namespace WSSale.Models
{
    public partial class Client
    {
        public Client()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
