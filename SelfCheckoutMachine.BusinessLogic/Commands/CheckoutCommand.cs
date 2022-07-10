using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.BusinessLogic.Extensions;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Entities;
using SelfCheckoutMachine.Enums;
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
            if (request.Model is null)
            {
                throw new UserException($"{nameof(request.Model)} cannot be null!");
            }

            var insertedCurrencies = request.Model.Inserted.Select(x => new Currency { Bill = x.Key.ConvertToBillType(), Amount = x.Value, ValueInHuf = decimal.Parse(x.Key) });

            if (request.Model.Price < 0)
            {
                throw new UserException("Price must be positive!");
            }

            if (request.Model.Inserted.Count == 0)
            {
                throw new UserException("Insert some bills!");
            }

            if (request.Model.Price == 0)
            {
                return request.Model.Inserted;
            }
            
            var sumOfInserted = insertedCurrencies.Sum(x => x.ValueInHuf);

            if (request.Model.Price > sumOfInserted)
            {
                throw new Exception($"The price is {request.Model.Price}, you inserted {sumOfInserted}, it's {request.Model.Price - sumOfInserted} less, please insert more bills!");
            }

            if (request.Model.Price == sumOfInserted)
            {
                return new Dictionary<string, int>();
            }

            var existingCurrencies = await DataContext.Currencies.ToListAsync(cancellationToken: cancellationToken);

            AddBills(insertedCurrencies, existingCurrencies);
            var changeInHuf = sumOfInserted - request.Model.Price;

            var result = GetCombinations(GetPossibleBillsInDescendingOrder(existingCurrencies, changeInHuf), changeInHuf);

            if (result != null)
            {
                foreach (var value in result)
                {
                    var bill = (BillType)value;
                    existingCurrencies.First(x => x.Bill == bill).Amount--;
                }

                await DataContext.SaveChangesAsync(cancellationToken);

                return await DataContext.Currencies.Where(x => x.Amount > 0).ToDictionaryAsync(x => x.Bill.ToString("D"), x => x.Amount, cancellationToken: cancellationToken);
            }

            throw new UserException("Don't have the necessary bills for change!");
        }

        public static IEnumerable<int> GetCombinations(int[] set, decimal sum, List<int> values = null)
        {
            if (values == null)
            {
                values = new List<int>();
            }

            for (var i = 0; i < set.Length; i++)
            {
                var left = sum - set[i];
                values.Add(set[i]);
                if (left == 0)
                {
                    return values;
                }
                else
                {
                    var possible = set.Skip(i + 1).Where(n => n <= left).ToArray();
                    if (possible.Length > 0)
                    {
                        return GetCombinations(possible, left, values);
                    }
                }
            }

            return null;
        }

        public static int[] GetPossibleBillsInDescendingOrder(List<Currency> currencies, decimal changeInHuf)
        {
            var retVal = new List<int>();
            foreach (var curr in currencies.Where(x => x.ValueInHuf <= changeInHuf).OrderByDescending(x => x.ValueInHuf))
            {
                for (int i = 0; i < curr.Amount; i++)
                {
                    retVal.Add((int)curr.Bill);
                }
            }

            return retVal.ToArray();
        }
    }
}
