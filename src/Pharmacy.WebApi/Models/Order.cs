using Pharmacy.WebApi.Common;
using Pharmacy.WebApi.Enums;

namespace Pharmacy.WebApi.Models
{
    public class Order : Auditable
    {
        public long DrugId { get; set; }
        public virtual Drug Drug { get; set; } = null!;
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }
        public int? CardNumber { get; set; }
    }
}
