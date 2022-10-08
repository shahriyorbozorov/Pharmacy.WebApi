using Pharmacy.WebApi.Common;

namespace Pharmacy.WebApi.Models
{
    public class Drug : Auditable
    {
        public string Name { get; set; } = String.Empty;
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
