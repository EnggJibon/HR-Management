using System;
using System.Collections.Generic;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class LeavePolicyModel : BaseDomainModel<LeavePolicyModel>
    {
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public DateTime? EffectiveDate { get; set; }

        public IEnumerable<LeaveTypeModel> LeaveTypes { get; set; }
    }
}
