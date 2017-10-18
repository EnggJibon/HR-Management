using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IDesignationRepository : IBaseRepository<Designation>
    {

    }

    public class DesignationRepository : BaseRepository<Designation, ERP_HRM>, IDesignationRepository
    {
        private readonly ERP_HRM _dbContextHRM;

        public DesignationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _dbContextHRM = new ERP_HRM();
        }

        public override IEnumerable<Designation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
