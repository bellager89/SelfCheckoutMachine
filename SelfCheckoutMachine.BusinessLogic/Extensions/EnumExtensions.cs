using SelfCheckoutMachine.Enums;

namespace SelfCheckoutMachine.BusinessLogic.Extensions
{
    public static class EnumExtensions
    {
        public static decimal ValueInHuf(this EurBillType billType, decimal exchangeRate)
        {
            return billType switch
            {
                EurBillType.EUR1 => exchangeRate,
                EurBillType.EUR2 => 2 * exchangeRate,
                EurBillType.EUR5 => 5 * exchangeRate,
                EurBillType.EUR10 => 10 * exchangeRate,
                EurBillType.EUR20 => 20 * exchangeRate,
                EurBillType.EUR50 => 50 * exchangeRate,
                EurBillType.EUR100 => 100 * exchangeRate,
                EurBillType.EUR200 => 200 * exchangeRate,
                EurBillType.EUR500 => 500 * exchangeRate,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
