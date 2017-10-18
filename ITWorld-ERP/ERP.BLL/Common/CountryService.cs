using ERP.Common.Domain;
using ERP.DAL.Common;
using ERP.DAL.Common.Repository;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Common
{
    public partial interface ICountryService : IBaseService<CountryModel, Country>
    {
    }

    public class CountryService : BaseService<CountryModel, Country>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
            : base(countryRepository)
        {
            _countryRepository = countryRepository;
        }
    }
}
