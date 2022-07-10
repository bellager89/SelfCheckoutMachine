using System.ComponentModel.DataAnnotations;

namespace SelfCheckoutMachine.Enums
{
    public enum BillType
    {
        FT1 = 1,
        FT2 = 2,
        FT5 = 5,
        FT10 = 10,
        FT20 = 20,
        FT50 = 50,
        FT100 = 100,
        FT200 = 200,
        FT500 = 500,
        FT1000 = 1000,
        FT2000 = 2000,
        FT5000 = 5000,
        FT10000 = 10000,
        FT20000 = 20000,
    }

    public enum EurBillType
    {
        EUR1 = 1,
        EUR2 = 2,
        EUR5 = 5,
        EUR10 = 10,
        EUR20 = 20,
        EUR50 = 50,
        EUR100 = 100,
        EUR200 = 200,
        EUR500 = 500,
    }
}