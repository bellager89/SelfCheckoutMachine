using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.BusinessLogic.Extensions;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Entities;

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
            var currencies = request.Values.Select(x => new Currency { Bill = x.Key.ConvertToBillType(), Amount = x.Value, ValueInHuf = decimal.Parse(x.Key) });
            var existingCurrencies = await DataContext.Currencies.ToListAsync(cancellationToken: cancellationToken);
            AddBills(currencies, existingCurrencies);

            await DataContext.SaveChangesAsync(cancellationToken);

            return await DataContext.Currencies.Where(x => x.Amount > 0).ToDictionaryAsync(x => x.Bill.ToString("D"), x => x.Amount, cancellationToken: cancellationToken);
        }
    }
}
