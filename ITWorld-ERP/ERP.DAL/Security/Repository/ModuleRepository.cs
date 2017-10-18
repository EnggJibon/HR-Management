using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IModuleRepository : IBaseRepository<Module>
    {

    }

    public class ModuleRepository : BaseRepository<Module, ERP_Security>, IModuleRepository
    {
        public ModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
