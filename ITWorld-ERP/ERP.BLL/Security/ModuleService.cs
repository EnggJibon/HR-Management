using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IModuleService : IBaseService<ModuleModel, Module>
    {
    }

    public class ModuleService : BaseService<ModuleModel, Module>, IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
            : base(moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }
    }
}
