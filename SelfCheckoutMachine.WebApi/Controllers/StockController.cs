using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.BusinessLogic.Commands;
using SelfCheckoutMachine.BusinessLogic.Queries;
using SelfCheckoutMachine.Models;

namespace SelfCheckoutMachine.WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IMediator _mediator;
        public StockController(ILogger<StockController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Dictionary<string, int>> Get()
        {
            var query = new GetStockQuery();
            var result = await _mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<Dictionary<string, int>> Post([FromBody] Dictionary<string, int> values)
        {
            var query = new AddStockCommand { Values = values };
            var result = await _mediator.Send(query);

            return result;
        }

        [HttpPost(nameof(Checkout))]
        public async Task<Dictionary<string, int>> Checkout([FromBody] CheckoutModel model)
        {
            throw new NotImplementedException();
        }
    }
}