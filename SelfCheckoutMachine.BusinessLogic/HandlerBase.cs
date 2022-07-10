using SelfCheckoutMachine.DataAccess;

namespace SelfCheckoutMachine.BusinessLogic
{
    public class HandlerBase
    {
        protected readonly DataContext DataContext;
        public HandlerBase(DataContext dataContext)
        {
            DataContext = dataContext;

        }
    }
}
