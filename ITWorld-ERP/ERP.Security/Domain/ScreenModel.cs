using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class ScreenModel : BaseDomainModel<ScreenModel>
    {
        public string ScreenCode { get; set; }
        public long ModuleId { get; set; }
        public string Title { get; set; }
        public string ScreenTitle { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
        public string ModuleName { get; set; }

        public ModuleModel Module { get; set; }
    }
}
