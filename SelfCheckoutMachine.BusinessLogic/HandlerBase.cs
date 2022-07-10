using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.BusinessLogic.Extensions;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Entities;
using SelfCheckoutMachine.Enums;
using SelfCheckoutMachine.Models;

namespace SelfCheckoutMachine.BusinessLogic
{
    public class HandlerBase
    {
        protected readonly DataContext DataContext;
        public HandlerBase(DataContext dataContext)
        {
            DataContext = dataContext;

        }
        protected async Task<Dictionary<string, int>> Checkout(CheckoutModel model, CancellationToken cancellationToken)
        {
            if (model is null)
            {
                throw new UserException($"{nameof(model)} cannot be null!");
            }

            var insertedCurrencies = model.Inserted.Select(x => new Currency { Bill = x.Key.ConvertToBillType(), Amount = x.Value, ValueInHuf = decimal.Parse(x.Key) });

            if (model.Price < 0)
            {
                throw new UserException("Price must be positive!");
            }

            if (model.Inserted.Count == 0)
            {
                throw new UserException("Insert some bills!");
            }

            if (model.Price == 0)
            {
                return model.Inserted;
            }

            var sumOfInserted = insertedCurrencies.Sum(x => x.ValueInHuf * x.Amount);

            if (model.Price > sumOfInserted)
            {
                throw new Exception($"The price is {model.Price}, you inserted {sumOfInserted}, it's {model.Price - sumOfInserted} less, please insert more bills!");
            }

            if (model.Price == sumOfInserted)
            {
                return new Dictionary<string, int>();
            }

            var existingCurrencies = await DataContext.Currencies.ToListAsync(cancellationToken: cancellationToken);

            AddBills(insertedCurrencies, existingCurrencies);
            var changeInHuf = sumOfInserted - model.Price;

            var result = GetBestCombination(GetPossibleBillsInDescendingOrder(existingCurrencies, changeInHuf), changeInHuf);

            if (result != null)
            {
                foreach (var value in result)
                {
                    var bill = (BillType)value;
                    existingCurrencies.First(x => x.Bill == bill).Amount--;
                }

                await DataContext.SaveChangesAsync(cancellationToken);

                return result.GroupBy(x => (BillType)x).ToDictionary(x => x.Key.ToString("D"), x => x.Count());
            }

            throw new UserException("Don't have the necessary bills for change!");
        }

        protected void AddBills(IEnumerable<Currency> currencies, List<Currency> existingCurrencies)
        {
            foreach (var curr in currencies)
            {
                var existingCurr = existingCurrencies.FirstOrDefault(x => x.Bill == curr.Bill);
                if (existingCurr != null)
                {
                    existingCurr.Amount += curr.Amount;
                }
                else
                {
                    DataContext.Currencies.Add(curr);
                    existingCurrencies.Add(curr);
                }
            }
        }

        protected static IEnumerable<int> GetBestCombination(int[] set, decimal sum, List<int> values = null)
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
                        return GetBestCombination(possible, left, values);
                    }
                }
            }

            return null;
        }

        protected static int[] GetPossibleBillsInDescendingOrder(List<Currency> currencies, decimal changeInHuf)
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
