using SelfCheckoutMachine.Enums;

namespace SelfCheckoutMachine.BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public static BillType ConvertToBillType(this string value)
        {
            const string errorMessage = "is not valid, acceptable values: 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000";
            if (int.TryParse(value, out int intVal))
            {
                try
                {
                    return (BillType)intVal;
                }
                catch
                {
                    throw new UserException($"{value} {errorMessage}");
                }
            }

            throw new UserException($"{value} {errorMessage}");
        }

        public static EurBillType ConvertToEurBillType(this string value)
        {
            const string errorMessage = "is not valid, acceptable values: 1, 2, 5, 10, 20, 50, 100, 200, 500";
            if (int.TryParse(value, out int intVal))
            {
                try
                {
                    return (EurBillType)intVal;
                }
                catch
                {
                    throw new UserException($"{value} {errorMessage}");
                }
            }

            throw new UserException($"{value} {errorMessage}");
        }
    }
}
