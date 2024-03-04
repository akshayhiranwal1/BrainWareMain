namespace Api.Models
{
    using System.Security.AccessControl;

    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProductViewModel> OrderProducts { get; set; }

    }

    public class OrderProductViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual OrderViewModel Order { get; set; } 
        public virtual ProductViewModel Product { get; set; }
    }

    public class ProductViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }

    public class OrderSummary
    {
        public string name { get; set; }
        public decimal total { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}