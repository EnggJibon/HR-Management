using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IEmployeeFamilyMemberRepository : IBaseRepository<EmployeeFamilyMember>
    {

    }

    public class EmployeeFamilyMemberRepository : BaseRepository<EmployeeFamilyMember, ERP_HRM>, IEmployeeFamilyMemberRepository
    {
        public EmployeeFamilyMemberRepository(IUnitOfWork unitOfWork)
                : base(unitOfWork)
            {

            }
        public override IEnumerable<EmployeeFamilyMember> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
