using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface IAnnualHolidayRepository : IBaseRepository<AnnualHoliday>
    {

    }

    public class AnnualHolidayRepository : BaseRepository<AnnualHoliday, ERP_HRM>, IAnnualHolidayRepository
    {
        public AnnualHolidayRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<AnnualHoliday> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }


    }
}
