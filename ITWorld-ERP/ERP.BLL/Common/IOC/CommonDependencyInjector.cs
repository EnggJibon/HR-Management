using ERP.Common.Mapping;
using ERP.Security.Mapping;
using ERP.Utilities.Infrastructure.Contract;
using Ninject;

namespace ERP.BLL.Common.IOC 
{
    public partial class CommonDependencyInjector : IDependencyInjector
	{
		public void Inject(object container)
        {
            var kernel = container as IKernel;

            kernel.Load<CommonRepositoryModule>();
            kernel.Load<CommonServiceModule>();

            CommonAutoMapperBootStrapper.Initialize();
         }
	}
}

