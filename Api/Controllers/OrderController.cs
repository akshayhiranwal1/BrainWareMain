namespace Api.Controllers
{
    using Api.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _service;
        public OrderController(IOrder service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("orders")]

        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _service.GetOrders());
        }

        //[HttpGet]
        //[Route("order/{id}")]

        //public IEnumerable<OrderViewModel> GetOrders(int id = 1)
        //{
        //    return new NotImplementedException();
        //}
    }
}
