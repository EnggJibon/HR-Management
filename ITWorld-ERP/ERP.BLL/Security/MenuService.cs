using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IMenuService : IBaseService<MenuModel, Menu>
    {
        List<MenuModel> GetPermittedMenus(long roleId);
    }

    public class MenuService : BaseService<MenuModel, Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleWiseScreenPermissionService _roleWiseScreenPermissionService;
        private readonly IScreenService _screenService;

        public MenuService(IMenuRepository menuRepository, IRoleWiseScreenPermissionService roleWiseScreenPermissionService, IScreenService screenService)
            : base(menuRepository)
        {
            _menuRepository = menuRepository;
            _roleWiseScreenPermissionService = roleWiseScreenPermissionService;
            _screenService = screenService;
        }

        public MenuModel GetMenu(long screenId)
        {
            return Mapper.Map<MenuModel>(_menuRepository.GetMenu(screenId));
        }

        public List<MenuModel> GetParentMenus()
        {
            return Mapper.Map<List<MenuModel>>(_menuRepository.GetParentMenus());
        }

        public List<MenuModel> GetPermittedMenus(long roleId)
        {
            //var roleWiseMenuList = Mapper.Map<List<RoleWiseMenu>, List<RoleWiseScreenPermissionModel>>(_roleWiseMenuRepository.GetRoleWiseMenulist(roleId)
            //                                                                                            .OrderBy(s => s.Menu.ParentId)
            //                                                                                            .ThenBy(s => s.Menu.MenuOrder)
            //                                                                                            .ToList());
            //foreach (var roleWiseMenu in roleWiseMenuList)
            //{
            //    roleWiseMenu.Menu = _menuService.GetMenu(roleWiseMenu.MenuId);
            //    roleWiseMenu.Menu.ParentMenu = _menuService.GetMenu(roleWiseMenu.Menu.ParentId);
            //    if (roleWiseMenu.Menu.PageId != null)
            //    {
            //        roleWiseMenu.Menu.Page = _pageService.GetPage((long)roleWiseMenu.Menu.PageId);
            //    }
            //}
            //return roleWiseMenuList;

            var permittedMenuList = new List<MenuModel>();

            var parentMenus = GetParentMenus();

            permittedMenuList.AddRange(parentMenus);

            var roleWiseScreenList = _roleWiseScreenPermissionService.GetRoleWiseScreenList(roleId);

            foreach (var roleWiseScreen in roleWiseScreenList)
            {
                var menu = GetMenu(roleWiseScreen.ScreenId);

                if (menu != null && menu.IsActive && menu.IsDeleted == false)
                {
                    menu.Screen = _screenService.GetById(menu.ScreenId);

                    if (menu.ParentMenuId > 0)
                    {
                        menu.ParentMenu = GetById(menu.ParentMenuId);

                        //if (menu.ParentMenu != null)
                        //{
                        //    //menu.ParentMenu.Screen = _screenService.GetById(menu.ParentMenu.ScreenId);
                        //    if (permittedMenuList.FirstOrDefault(m => m.Id == menu.ParentMenuId) == null)
                        //    {
                        //        permittedMenuList.Add(menu.ParentMenu);
                        //    }
                        //    if (menu.ParentMenu.ParentMenuId > 0)
                        //    {
                        //        menu.ParentMenu.ParentMenu = GetById(menu.ParentMenu.ParentMenuId);
                        //        if (menu.ParentMenu.ParentMenu != null && permittedMenuList.FirstOrDefault(m => m.Id == menu.ParentMenu.ParentMenu.ParentMenuId) == null)
                        //        {
                        //            permittedMenuList.Add(menu.ParentMenu.ParentMenu);
                        //        }
                        //    }
                        //}
                    }
                    permittedMenuList.Add(menu);
                }
            }

            return permittedMenuList.OrderBy(m => m.ParentMenuId).ThenBy(m => m.MenuOrder).ToList();
        }
    }
}
