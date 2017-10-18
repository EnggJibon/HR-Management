using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class MenuModel : BaseDomainModel<MenuModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte MenuOrder { get; set; }
        public string MenuIcon { get; set; }
        public long? ParentMenuId { get; set; }
        public long? ScreenId { get; set; }

        public MenuModel ParentMenu { get; set; }
        public ScreenModel Screen { get; set; }
    }
}
