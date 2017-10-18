using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure.Contract;
using ERP.Utilities.Infrastructure;






namespace ERP.BLL.HRM
{

    public partial interface IRoosterSetupService : IBaseService<RoosterSetupModel,RoosterSetup>
    {
    }

    public class RoosterSetupService:BaseService<RoosterSetupModel,RoosterSetup>,IRoosterSetupService
    {
       
        private readonly IRoosterSetupRepository _roosterSetupRepository;

        public RoosterSetupService(IRoosterSetupRepository roosterSetupRepository)
            : base(roosterSetupRepository)
        {
            _roosterSetupRepository = roosterSetupRepository;
        }

    }
}


