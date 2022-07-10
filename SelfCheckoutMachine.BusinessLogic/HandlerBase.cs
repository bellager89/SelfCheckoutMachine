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
    }
}
