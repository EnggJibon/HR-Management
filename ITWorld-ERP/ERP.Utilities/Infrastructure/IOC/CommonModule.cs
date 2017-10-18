using ERP.Utilities.Infrastructure.Contract;
using Ninject.Modules;

namespace ERP.Utilities.Infrastructure.IOC
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IConnectionStringProvider>().To<ConnectionStringProvider>();
        }
    }
}
