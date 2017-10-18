using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IWeekendCategoryRepository : IBaseRepository<WeekendCategory>
    {

    }

    public class WeekendCategoryRepository : BaseRepository<WeekendCategory, ERP_HRM>, IWeekendCategoryRepository
    {
        public WeekendCategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        public override IEnumerable<WeekendCategory> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
