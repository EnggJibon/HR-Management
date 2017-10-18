using ERP.Security.Mapping;
using ERP.Utilities.Infrastructure.Contract;
using Ninject;

namespace ERP.BLL.Security.IOC 
{
    public partial class SecurityDependencyInjector : IDependencyInjector
	{
		public void Inject(object container)
        {
            var kernel = container as IKernel;

            kernel.Load<SecurityRepositoryModule>();
            kernel.Load<SecurityServiceModule>();

            SecurityAutoMapperBootStrapper.Initialize();
         }
	}
}

