using SelfCheckoutMachine.BusinessLogic.Extensions;
using SelfCheckoutMachine.DataAccess;
using SelfCheckoutMachine.Entities;

namespace SelfCheckoutMachine.BusinessLogic
{
    public class HandlerBase
    {
        protected readonly DataContext DataContext;
        public HandlerBase(DataContext dataContext)
        {
            DataContext = dataContext;

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
                    curr.ValueInEur = curr.Bill.ValueInEur(300);
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
