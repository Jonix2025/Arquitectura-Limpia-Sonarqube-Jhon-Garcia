using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.UseCases;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderUseCase createOrderUseCase;

        public OrdersController(CreateOrderUseCase createOrderUseCase)
        {
            this.createOrderUseCase = createOrderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string customer, string product, int qty, decimal price)
        {
            var order = await createOrderUseCase.ExecuteAsync(customer, product, qty, price);
           
            return Ok(order);
        }
    }
}