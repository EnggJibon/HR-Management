using ERP.DAL.Common.Repository;
using Ninject.Modules;

namespace ERP.BLL.Common.IOC
{
    public partial class CommonRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICountryRepository>().To<CountryRepository>();
        }
    }
}

