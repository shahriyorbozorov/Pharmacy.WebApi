using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.Repositories
{
    public class DrugRepository : GenericRepository<Drug>, IDrugRepository
    {
        public DrugRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
