
namespace Application.ViewModel
{
    public class CustomerExchangeViewModel
    {
        public string ID { get; set; }
        public string CustomerID { get; set; }
        public int FromExchange { get; set; }
        public int ToExchange { get; set; }
        public float Amount { get; set; }
        public float ExchangeRate { get; set; }
        public float ConvertedValue { get; set; }
    }
}
