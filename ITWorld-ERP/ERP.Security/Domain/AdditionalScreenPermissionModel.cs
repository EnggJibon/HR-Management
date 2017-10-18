using System;
using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class AdditionalScreenPermissionModel : BaseDomainModel<AdditionalScreenPermissionModel>
    {
        public long UserId { get; set; }
        public long ScreenId { get; set; }
        public long ModuleId { get; set; }
        public string AccessRight { get; set; }
        public string CanRead { get; set; }
        public string CanCreate { get; set; }
        public string CanUpdate { get; set; }
        public string CanDelete { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ModuleName { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenTitle { get; set; }
        public string UserName { get; set; }


        public UserInformationModel UserInformation { get; set; }
        public ScreenModel Screen { get; set; }
    }
}
