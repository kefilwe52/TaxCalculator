using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository.Repositories;

public class PostalCodeInfoRepository : GenericRepository<PostalCodeInfo>, IPostalCodeInfoRepository
{
    public PostalCodeInfoRepository(DbContext context) : base(context)
    {
    }
}