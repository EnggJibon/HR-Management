using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Common.Repository
{
    public partial interface ICountryRepository : IBaseRepository<Country>
    {

    }

    public class CountryRepository : BaseRepository<Country, ERP_Common>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
