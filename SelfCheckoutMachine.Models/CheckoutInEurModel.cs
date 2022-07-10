
namespace SelfCheckoutMachine.Models
{
    public class CheckoutInEurModel
    {
        public Dictionary<string, int> Inserted { get; set; }
        public decimal Price { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}