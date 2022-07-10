using SelfCheckoutMachine.Enums;

namespace SelfCheckoutMachine.BusinessLogic.Extensions
{
    public static class EnumExtensions
    {
        public static decimal ValueInHuf(this BillType billType)
        {
            return billType switch
            {
                BillType.FT1 => 1,
                BillType.FT2 => 2,
                BillType.FT5 => 5,
                BillType.FT10 => 10,
                BillType.FT20 => 20,
                BillType.FT50 => 50,
                BillType.FT100 => 100,
                BillType.FT200 => 200,
                BillType.FT500 => 500,
                BillType.FT1000 => 1000,
                BillType.FT2000 => 2000,
                BillType.FT5000 => 5000,
                BillType.FT10000 => 10000,
                BillType.FT20000 => 20000,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public static decimal ValueInEur(this BillType billType, decimal exchangeRate)
        {
            return billType switch
            {
                BillType.FT1 => 1 / exchangeRate,
                BillType.FT2 => 2 / exchangeRate,
                BillType.FT5 => 5 / exchangeRate,
                BillType.FT10 => 10 / exchangeRate,
                BillType.FT20 => 20 / exchangeRate,
                BillType.FT50 => 50 / exchangeRate,
                BillType.FT100 => 100 / exchangeRate,
                BillType.FT200 => 200 / exchangeRate,
                BillType.FT500 => 500 / exchangeRate,
                BillType.FT1000 => 1000 / exchangeRate,
                BillType.FT2000 => 2000 / exchangeRate,
                BillType.FT5000 => 5000 / exchangeRate,
                BillType.FT10000 => 10000 / exchangeRate,
                BillType.FT20000 => 20000 / exchangeRate,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
