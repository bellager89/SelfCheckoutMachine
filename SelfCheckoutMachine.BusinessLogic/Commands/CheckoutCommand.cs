using MediatR;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Models;

namespace SelfCheckoutMachine.BusinessLogic.Commands
{
    public class CheckoutCommand : IRequest<Dictionary<string, int>>
    {
        public CheckoutModel Model { get; set; }
    }

    public class CheckoutCommandHandler : HandlerBase, IRequestHandler<CheckoutCommand, Dictionary<string, int>>
    {

        public CheckoutCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Dictionary<string, int>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            return await Checkout(request.Model, cancellationToken);
        }
    }
}
