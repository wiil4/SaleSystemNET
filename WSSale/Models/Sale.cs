using System;
using System.Collections.Generic;

namespace WSSale.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Concepts = new HashSet<Concept>();
        }

        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int? IdClient { get; set; }
        public decimal? Total { get; set; }

        public virtual Client? IdClientNavigation { get; set; }
        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
