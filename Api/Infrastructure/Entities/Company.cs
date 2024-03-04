using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Entities
{
    public partial class Company
    {
        public Company()
        {
            Orders = new HashSet<Order>();
        }

        public int CompanyId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
