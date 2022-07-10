using System.ComponentModel.DataAnnotations;

namespace SelfCheckoutMachine.Enums
{
    public enum BillType
    {
        [Display(Name = "1Ft")]
        FT1 = 1,
        [Display(Name = "2Ft")]
        FT2 = 2,
        [Display(Name = "5Ft")]
        FT5 = 5,
        [Display(Name = "10Ft")]
        FT10 = 10,
        [Display(Name = "20Ft")]
        FT20 = 20,
        [Display(Name = "50Ft")]
        FT50 = 50,
        [Display(Name = "100Ft")]
        FT100 = 100,
        [Display(Name = "200Ft")]
        FT200 = 200,
        [Display(Name = "500Ft")]
        FT500 = 500,
        [Display(Name = "1000Ft")]
        FT1000 = 1000,
        [Display(Name = "2000Ft")]
        FT2000 = 2000,
        [Display(Name = "5000Ft")]
        FT5000 = 5000,
        [Display(Name = "10000Ft")]
        FT10000 = 10000,
        [Display(Name = "20000Ft")]
        FT20000 = 20000,
    }
}