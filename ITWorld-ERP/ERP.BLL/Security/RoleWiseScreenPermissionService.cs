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
    public partial interface IRoleWiseScreenPermissionService : IBaseService<RoleWiseScreenPermissionModel, RoleWiseScreenPermission>
    {
        List<RoleWiseScreenPermissionModel> GetRoleWiseScreenList(long roleId);
        List<RoleWiseScreenPermissionModel> GetRoleWiseScreenPermissionList(long? id, long? roleId, long? moduleId, long? screenId);
        RoleWiseScreenPermissionModel GetRoleWiseScreenPermissionDetails(long id);
    }

    public class RoleWiseScreenPermissionService : BaseService<RoleWiseScreenPermissionModel, RoleWiseScreenPermission>, IRoleWiseScreenPermissionService
    {
        private readonly IRoleWiseScreenPermissionRepository _roleWiseScreenPermissionRepository;

        public RoleWiseScreenPermissionService(IRoleWiseScreenPermissionRepository roleWiseScreenPermissionRepository)
            : base(roleWiseScreenPermissionRepository)
        {
            _roleWiseScreenPermissionRepository = roleWiseScreenPermissionRepository;
        }

        public List<RoleWiseScreenPermissionModel> GetRoleWiseScreenList(long roleId)
        {
            return Mapper.Map<List<RoleWiseScreenPermissionModel>>(_roleWiseScreenPermissionRepository.GetRoleWiseScreenList(roleId));
        }

        public List<RoleWiseScreenPermissionModel> GetRoleWiseScreenPermissionList(long? id, long? roleId, long? moduleId, long? screenId)
        {
            var screenPermissionList = _roleWiseScreenPermissionRepository.GetRoleWiseScreenPermissions(id, roleId, moduleId, screenId);
            return Mapper.Map<List<RoleWiseScreenPermissionModel>>(screenPermissionList);
        }

        public RoleWiseScreenPermissionModel GetRoleWiseScreenPermissionDetails(long id)
        {
            var screenPermission = _roleWiseScreenPermissionRepository.GetRoleWiseScreenPermissions(id, null, null, null).FirstOrDefault();
            return Mapper.Map<RoleWiseScreenPermissionModel>(screenPermission);
        }
    }
}
