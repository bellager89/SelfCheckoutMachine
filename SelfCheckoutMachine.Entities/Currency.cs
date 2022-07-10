using SelfCheckoutMachine.Enums;

namespace SelfCheckoutMachine.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public BillType Bill { get; set; }
        public decimal ValueInHuf { get; set; }
        public int Amount { get; set; }
    }
}