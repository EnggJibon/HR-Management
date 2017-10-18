using System;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class RosterInformationModel : BaseDomainModel<RosterInformationModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
