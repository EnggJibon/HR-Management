using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IDesignationService : IBaseService<DesignationModel, Designation>
    {
    }

    public class DesignationService : BaseService<DesignationModel, Designation>, IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IDesignationRepository designationRepository)
            : base(designationRepository)
        {
            _designationRepository = designationRepository;
        }
    }
}
