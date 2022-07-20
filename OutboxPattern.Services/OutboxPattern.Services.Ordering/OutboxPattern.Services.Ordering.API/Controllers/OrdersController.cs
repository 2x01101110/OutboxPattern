using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OutboxPattern.Services.Ordering.API.Application.Contracts;
using OutboxPattern.Services.Ordering.API.Controllers.Models;

namespace OutboxPattern.Services.Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRequestClient<ICreateOrder> _createOrderRequest;

        public OrdersController(
            IRequestClient<ICreateOrder> createOrderRequest)
        {
            _createOrderRequest = createOrderRequest;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderModel model)
        {
            Response response = await _createOrderRequest.GetResponse<IOrderCreated, IOrderRejected>(new
            {
                model.Id,
                model.Items
            });

            return response switch
            {
                (_, IOrderCreated created) => Ok(created),
                (_, IOrderRejected rejected) => BadRequest(rejected),
                _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
            };
        }
    }
}
