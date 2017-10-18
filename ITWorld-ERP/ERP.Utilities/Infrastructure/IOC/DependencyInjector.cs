using ERP.Utilities.Infrastructure.Contract;
using Ninject;

namespace ERP.Utilities.Infrastructure.IOC
{
    public class DependencyInjector : IDependencyInjector
    {
        public void Inject(object container)
        {
            var kernel = container as IKernel;
            kernel.Load<CommonModule>();
         }
    }
}
