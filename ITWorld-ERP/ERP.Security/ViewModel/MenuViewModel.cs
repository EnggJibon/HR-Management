using System.Collections.Generic;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    class MenuViewModel
    {
        public IEnumerable<MenuModel> Menus { get; set; }
        public MenuModel Menu { get; set; }
    }
}
