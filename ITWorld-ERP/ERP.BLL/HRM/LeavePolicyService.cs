using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface ILeavePolicyService : IBaseService<LeavePolicyModel, LeavePolicy>
    {

    }

    public class LeavePolicyService : BaseService<LeavePolicyModel, LeavePolicy>, ILeavePolicyService
    {
        private readonly ILeavePolicyRepository _leavePolicyRepository;

        public LeavePolicyService(ILeavePolicyRepository leavePolicyRepository)
            : base(leavePolicyRepository)
        {
            _leavePolicyRepository = leavePolicyRepository;
        }
    }
}
