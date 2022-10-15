using Pharmacy.WebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.WebApi.ViewModels.Orders
{
    public class OrderCreateModel
    {
        [Required]
        public long DrugId { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        public int? CardNumber { get; set; }
    }
}
