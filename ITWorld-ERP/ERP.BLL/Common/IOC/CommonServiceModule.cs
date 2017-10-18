using Ninject.Modules;

namespace ERP.BLL.Common.IOC 
{
    public partial class CommonServiceModule : NinjectModule
	{
		public override void Load()
        {
            Bind<ICountryService>().To<CountryService>();
		}
	}
}

