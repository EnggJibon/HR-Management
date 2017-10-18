using ERP.HRM.Mapping;
using ERP.Utilities.Infrastructure.Contract;
using Ninject;

namespace ERP.BLL.HRM.IOC 
{
	public partial class HRMDependencyInjector : IDependencyInjector
	{
		public void Inject(object container)
        {
            var kernel = container as IKernel;
            
            kernel.Load<HRMRepositoryModule>();
            kernel.Load<HRMServiceModule>();

			HRMAutoMapperBootStrapper.Initialize();
         }
	}
}

