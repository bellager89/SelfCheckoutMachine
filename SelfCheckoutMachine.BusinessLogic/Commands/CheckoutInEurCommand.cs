using MediatR;
using SelfCheckoutMachine.BusinessLogic.Extensions;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Enums;
using SelfCheckoutMachine.Models;

namespace SelfCheckoutMachine.BusinessLogic.Commands
{
    public class CheckoutInEurCommand : IRequest<Dictionary<string, int>>
    {
        public CheckoutInEurModel Model { get; set; }
    }

    public class CheckoutInEurCommandHandler : HandlerBase, IRequestHandler<CheckoutInEurCommand, Dictionary<string, int>>
    {

        public CheckoutInEurCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Dictionary<string, int>> Handle(CheckoutInEurCommand request, CancellationToken cancellationToken)
        {
            var checkoutModel = new CheckoutModel
            {
                Price = request.Model.Price,
                Inserted = ExchangeEurToHuf(request.Model.Inserted, request.Model.ExchangeRate)
            };
            return await Checkout(checkoutModel, cancellationToken);
        }

        private Dictionary<string, int> ExchangeEurToHuf(Dictionary<string, int> eurBills, decimal exchangeRate)
        {
            var sumHuf = eurBills.Sum(e => e.Key.ConvertToEurBillType().ValueInHuf(exchangeRate));
            var retVal = new Dictionary<string, int>();

            foreach (var bill in Enum.GetValues(typeof(BillType)).Cast<BillType>().OrderByDescending(x => (int)x))
            {
                if (sumHuf < 1)
                {
                    break;
                }
                var valueInHuf = (int)bill;
                if ((int)bill <= sumHuf)
                {
                    retVal.Add(bill.ToString("D"), (int)(sumHuf / (int)bill));
                    sumHuf = sumHuf % (int)bill;
                }
            }

            return retVal;
        }
    }
}
