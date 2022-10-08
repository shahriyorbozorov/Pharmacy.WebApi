using Pharmacy.WebApi.Enums;

namespace Pharmacy.WebApi.ViewModels.Orders
{
    public class OrderViewModel
    {
        public string DrugName { get; set; } = String.Empty;
        public string UserFullName { get; set; } = String.Empty;
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }
        public int? CardNumber { get; set; }
    }
}
