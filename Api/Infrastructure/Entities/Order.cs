using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string Description { get; set; } = null!;
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
    }
}
