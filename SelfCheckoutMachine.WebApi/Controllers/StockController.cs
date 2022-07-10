using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.BusinessLogic.Commands;
using SelfCheckoutMachine.BusinessLogic.Queries;
using SelfCheckoutMachine.Models;

namespace SelfCheckoutMachine.WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
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

        [HttpGet(nameof(Stock))]
        public async Task<Dictionary<string, int>> Stock()
        {
            _logger.LogInformation("Getting stock information");
            var query = new GetStockQuery();
            var result = await _mediator.Send(query);

            return result;
        }

        [HttpPost(nameof(Stock))]
        public async Task<Dictionary<string, int>> Stock([FromBody] Dictionary<string, int> values)
        {
            _logger.LogInformation("Refill stock");
            var query = new AddStockCommand { Values = values };
            var result = await _mediator.Send(query);

            return result;
        }

        [HttpPost(nameof(Checkout))]
        public async Task<Dictionary<string, int>> Checkout([FromBody] CheckoutModel model)
        {
            _logger.LogInformation($"Checkout");
            var query = new CheckoutCommand { Model = model };
            var result = await _mediator.Send(query);

            return result;
        }

        [HttpGet(nameof(BlockedBills))]
        public async Task<List<string>> BlockedBills(int price)
        {
            _logger.LogInformation($"Getting blocked bills");
            var query = new BlockedBillsQuery { Price = price };
            var result = await _mediator.Send(query);

            return result;
        }

        [HttpPost(nameof(CheckoutInEur))]
        public async Task<Dictionary<string, int>> CheckoutInEur([FromBody] CheckoutInEurModel model)
        {
            _logger.LogInformation($"Checkout in EUR");
            var query = new CheckoutInEurCommand { Model = model };
            var result = await _mediator.Send(query);

            return result;
        }
    }
}