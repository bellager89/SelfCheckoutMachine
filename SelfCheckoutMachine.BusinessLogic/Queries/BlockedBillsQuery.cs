using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Enums;

namespace SelfCheckoutMachine.BusinessLogic.Queries
{
    public class BlockedBillsQuery : IRequest<List<string>>
    {
        public int Price { get; set; }
    }

    public class BlockedBillsQueryHandler : HandlerBase, IRequestHandler<BlockedBillsQuery, List<string>>
    {
        public BlockedBillsQueryHandler(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<List<string>> Handle(BlockedBillsQuery request, CancellationToken cancellationToken)
        {
            var existingCurrencies = await DataContext.Currencies.Where(x => x.Amount > 0).ToListAsync(cancellationToken);
            var retVal = new List<string>();

            foreach (var bill in Enum.GetValues(typeof(BillType)).Cast<BillType>())
            {
                var valueInHuf = (int)bill;
                if (valueInHuf <= request.Price)
                {
                    retVal.Add(bill.ToString("D"));
                    continue;
                }

                var result = GetBestCombination(GetPossibleBillsInDescendingOrder(existingCurrencies, request.Price), (int)bill - request.Price);
                if (result != null)
                {
                    retVal.Add(bill.ToString("D"));
                    continue;
                }

                break;
            }
            
            return retVal;
        }
    }
}
