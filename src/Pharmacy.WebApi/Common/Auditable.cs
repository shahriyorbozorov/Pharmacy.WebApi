namespace Pharmacy.WebApi.Common
{
    public class Auditable : BaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
