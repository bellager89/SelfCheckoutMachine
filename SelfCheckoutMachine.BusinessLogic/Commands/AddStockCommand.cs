using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.BusinessLogic.Extensions;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Entities;
using SelfCheckoutMachine.Enums;

namespace SelfCheckoutMachine.BusinessLogic.Commands
{
    public class AddStockCommand : IRequest<Dictionary<string, int>>
    {
        public Dictionary<string, int> Values { get; set; }
    }

    public class AddStockCommandHandler : HandlerBase, IRequestHandler<AddStockCommand, Dictionary<string, int>>
    {

        public AddStockCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Dictionary<string, int>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var currencies = request.Values.Select(x => new Currency { Bill = (BillType)int.Parse(x.Key), Amount = x.Value });
            var existingCurrencies = await DataContext.Currencies.ToListAsync(cancellationToken: cancellationToken);

            foreach (var curr in currencies)
            {
                var existingCurr = existingCurrencies.FirstOrDefault(x => x.Bill == curr.Bill);
                if (existingCurr != null)
                {
                    existingCurr.Amount += curr.Amount;
                }
                else
                {
                    curr.ValueInHuf = (decimal)curr.Bill;
                    curr.ValueInEur = curr.Bill.ValueInEur(300);
                    DataContext.Currencies.Add(curr);
                }
            }

            await DataContext.SaveChangesAsync(cancellationToken);

            return await DataContext.Currencies.Where(x => x.Amount > 0).ToDictionaryAsync(x => x.Bill.ToString("D"), x => x.Amount, cancellationToken: cancellationToken);
        }
    }
}
