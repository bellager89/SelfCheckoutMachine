using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.DataAccess;

namespace SelfCheckoutMachine.BusinessLogic.Queries
{
    public class GetStockQuery : IRequest<Dictionary<string, int>>
    {
    }

    public class GetStockQueryHandler : HandlerBase, IRequestHandler<GetStockQuery, Dictionary<string, int>>
    {
        public GetStockQueryHandler(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<Dictionary<string, int>> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            return await DataContext.Currencies.Where(x => x.Amount > 0).ToDictionaryAsync(x => x.Bill.ToString("D"), x => x.Amount, cancellationToken: cancellationToken);
        }
    }
}
