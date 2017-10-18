using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IMenuRepository : IBaseRepository<Menu>
    {
        Menu GetMenu(long screenId);
        List<Menu> GetParentMenus();
    }

    public class MenuRepository : BaseRepository<Menu, ERP_Security>, IMenuRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();

        public MenuRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Menu GetMenu(long screenId)
        {
            return _dbContextSecurity.Menus.FirstOrDefault(m => m.ScreenId == screenId);
        }

        public List<Menu> GetParentMenus()
        {
            return _dbContextSecurity.Menus.Where(m => m.ScreenId == null).ToList();
        }
    }
}
