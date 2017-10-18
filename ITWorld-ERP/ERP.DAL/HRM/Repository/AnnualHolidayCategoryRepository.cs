using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IAnnualHolidayCategoryRepository : IBaseRepository<AnnualHolidayCategory>
    {

    }

    public class AnnualHolidayCategoryRepository : BaseRepository<AnnualHolidayCategory, ERP_HRM>, IAnnualHolidayCategoryRepository
    {
        public AnnualHolidayCategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<AnnualHolidayCategory> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
