namespace Api.Infrastructure
{
    using System.Data;
    using System.Threading.Tasks;
    using Api.Infrastructure.Entities;
    using Api.Infrastructure.Repository;
    using Api.Interfaces;
    using Api.Models;
    using AutoMapper;

    public class OrderService:IOrder
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderSummary>> GetOrders()
        {
            var result = _unitOfWork.GetRepository<Orderproduct>().FindBy((i => 1 == 1), "Order", "Product");

            var tmp = result.ToList().Select(i => new
            {
                Id = i.OrderId,
                OrderName = i.Order.Description,
                Quantity = i.Quantity,
                ProductName = i.Product.Name,
                ProductPrice = i.Product.Price
            });

            return (from d in (from c in tmp
                              group c by c.Id)
                              select new OrderSummary() {
                                  name = d.FirstOrDefault().OrderName,
                                  total = d.Where(i=> i.Id == d.Key).Select(k=> k.Quantity * k.ProductPrice).Sum(),
                                  Products = d.Where(i=> i.Id == d.Key).Select(k=> new ProductViewModel() {
                                      Price = k.ProductPrice,
                                      Name = k.ProductName,
                                      Quantity = k.Quantity
                                  }).ToList()
                              }).ToList();
        }
        
        private List<OrderViewModel> GetOrdersForCompany(int CompanyId)
        {

            var database = new Database();

            // Get the orders
            var sql1 =
                "SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id";

            var reader1 = database.ExecuteReader(sql1);

            var values = new List<OrderViewModel>();
            
            while (reader1.Read())
            {
                var record1 = (IDataRecord) reader1;

                values.Add(new OrderViewModel()
                {
                    CompanyName = record1.GetString(0),
                    Description = record1.GetString(1),
                    OrderId = record1.GetInt32(2),
                    OrderProducts = new List<OrderProductViewModel>()
                });

            }

            reader1.Close();

            //Get the order products
            var sql2 =
                "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id";

            var reader2 = database.ExecuteReader(sql2);

            var values2 = new List<OrderProductViewModel>();

            while (reader2.Read())
            {
                var record2 = (IDataRecord)reader2;

                values2.Add(new OrderProductViewModel()
                {
                    OrderId = record2.GetInt32(1),
                    ProductId = record2.GetInt32(2),
                    Price = record2.GetDecimal(0),
                    Quantity = record2.GetInt32(3),
                    Product = new ProductViewModel()
                    {
                        Name = record2.GetString(4),
                        Price = record2.GetDecimal(5)
                    }
                });
             }

            reader2.Close();

            foreach (var order in values)
            {
                foreach (var orderproduct in values2)
                {
                    if (orderproduct.OrderId != order.OrderId)
                        continue;

                    order.OrderProducts.Add(orderproduct);
                    order.OrderTotal = order.OrderTotal + (orderproduct.Price * orderproduct.Quantity);
                }
            }

            return values;
        }
    }
}