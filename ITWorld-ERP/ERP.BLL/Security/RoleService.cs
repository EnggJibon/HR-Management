using System.Collections.Generic;
using AutoMapper;
using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IRoleService : IBaseService<RoleModel, Role>
    {
    }

    public class RoleService : BaseService<RoleModel, Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
            : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }       
    }
}
