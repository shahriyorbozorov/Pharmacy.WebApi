using System.ComponentModel.DataAnnotations;

namespace Pharmacy.WebApi.ViewModels.Drugs
{
    public class DrugCreateModel
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = String.Empty;

        [Required]
        public double Price { get; set; }

        [Required]
        public double Quantity { get; set; }
    }
}
