using AutoMapper;
using SecurityAdministration.BLL.ViewModels;
using SecurityAdministration.DAL;

namespace SecurityAdministration.BLL
{
    public class InitializeAutoMapper
    {
        public static void Initialize()
        {
            CreateModelsToViewModels();
            CreateViewModelsToModels();
        }

        private static void CreateModelsToViewModels()
        {
            Mapper.CreateMap<Designation, DesignationView>();
            Mapper.CreateMap<MenuItem, MenuItemView>();
            Mapper.CreateMap<UserInRole, UserInRoleView>();
        }

        private static void CreateViewModelsToModels()
        {
            Mapper.CreateMap<DesignationView, Designation>();
            Mapper.CreateMap<MenuItemView, MenuItem>();
            Mapper.CreateMap<UserInRoleView, UserInRole>();

            //.ForMember(c => c.CategoryPositions, option => option.Ignore())
            //.ForMember(c => c.Posts, option => option.Ignore());
        }
    }
}