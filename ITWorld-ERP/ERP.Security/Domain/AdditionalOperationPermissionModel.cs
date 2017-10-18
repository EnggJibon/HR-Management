using System;
using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class AdditionalOperationPermissionModel : BaseDomainModel<AdditionalOperationPermissionModel>
    {
        public long UserId { get; set; }
        public string ScreenOperationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ScreenOperationName { get; set; }
        public string Username { get; set; }
    }
}
