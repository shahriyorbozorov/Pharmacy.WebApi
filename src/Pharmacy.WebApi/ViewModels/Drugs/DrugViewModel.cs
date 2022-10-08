namespace Pharmacy.WebApi.ViewModels.Drugs
{
    public class DrugViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
