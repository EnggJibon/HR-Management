using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IEmployeeFamilyMemberService : IBaseService<EmployeeFamilyMemberModel, EmployeeFamilyMember>
    {
    }

    public class EmployeeFamilyMemberService : BaseService<EmployeeFamilyMemberModel, EmployeeFamilyMember>, IEmployeeFamilyMemberService
    {
        private readonly IEmployeeFamilyMemberRepository _employeeFamilyMemberRepository;

        public EmployeeFamilyMemberService(IEmployeeFamilyMemberRepository employeeFamilyMemberRepository)
            : base(employeeFamilyMemberRepository)
        {
            _employeeFamilyMemberRepository = employeeFamilyMemberRepository;
        }


    }
}


