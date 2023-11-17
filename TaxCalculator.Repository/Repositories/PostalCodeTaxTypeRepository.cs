using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository.Repositories
{
    public class PostalCodeTaxTypeRepository : GenericRepository<PostalCodeTaxType>, IPostalCodeTaxTypeRepository
    {
        public PostalCodeTaxTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
