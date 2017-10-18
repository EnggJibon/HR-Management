using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IWeekendRepository : IBaseRepository<Weekend>
    {

    }

    public class WeekendRepository : BaseRepository<Weekend, ERP_HRM>, IWeekendRepository
    {
        public WeekendRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<Weekend> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
