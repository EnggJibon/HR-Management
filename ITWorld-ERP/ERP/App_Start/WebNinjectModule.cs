using ERP.BLL.HRM;
using Ninject.Modules;

namespace ERP
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // UOWS
            //Bind<ILookupCacheService>()  
            //    .To<LookupCacheService>();
        }
    }
}
