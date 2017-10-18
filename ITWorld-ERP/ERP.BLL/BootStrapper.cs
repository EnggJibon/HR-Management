using ERP.BLL.Common.IOC;
using ERP.BLL.Security.IOC;
using ERP.BLL.HRM.IOC;
using ERP.Utilities.Infrastructure.Contract;
using ERP.Utilities.Infrastructure.IOC;
using Ninject;

namespace ERP.BLL
{
    public class BootStrapper
    {
        public static IKernel Initialize()
        {   
            IKernel kernel = new StandardKernel();
            ConfigureGeneric(kernel);
            ConfigureSecurityModule(kernel);
            ConfigureHRMModule(kernel);
            ConfigureCommonModule(kernel);
            return kernel;
        }

        private static IKernel ConfigureGeneric(IKernel kernel)
        {
            IDependencyInjector injector = new DependencyInjector();
            injector.Inject(kernel);
            AutoMapperBootstrapper.Initialize(kernel);
            return kernel;
        }

        private static IKernel ConfigureSecurityModule(IKernel kernel)
        {
            IDependencyInjector injector = new SecurityDependencyInjector();
            injector.Inject(kernel);
            return kernel;
        }

        private static IKernel ConfigureHRMModule(IKernel kernel)
        {
            IDependencyInjector injector = new HRMDependencyInjector();
            injector.Inject(kernel);
            return kernel;
        }

        private static IKernel ConfigureCommonModule(IKernel kernel)
        {
            IDependencyInjector injector = new CommonDependencyInjector();
            injector.Inject(kernel);
            return kernel;
        }
    }
}
