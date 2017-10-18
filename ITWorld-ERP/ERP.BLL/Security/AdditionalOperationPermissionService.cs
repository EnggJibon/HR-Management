using System.Collections.Generic;
using AutoMapper;
using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;


namespace ERP.BLL.Security
{
    public partial interface IAdditionalOperationPermissionService : IBaseService<AdditionalOperationPermissionModel, AdditionalOperationPermission>
    {
        List<AdditionalOperationPermissionModel> GetAdditionalOperationPermissionList(long? id, long? userId, long? screenOperationId);
    }

    public  class AdditionalOperationPermissionService : BaseService<AdditionalOperationPermissionModel, AdditionalOperationPermission>, IAdditionalOperationPermissionService
    {
         private readonly IAdditionalOperationPermissionRepository _additionalOperationPermissionRepository;

         public AdditionalOperationPermissionService(IAdditionalOperationPermissionRepository additionalOperationPermissionRepository)
             : base(additionalOperationPermissionRepository)
        {
            _additionalOperationPermissionRepository = additionalOperationPermissionRepository;
        }

         public List<AdditionalOperationPermissionModel> GetAdditionalOperationPermissionList(long? id, long? userId, long? screenOperationId)
         {
             var additionalOperationPermissionList = _additionalOperationPermissionRepository.GetRoleWiseOperationPermissions(id, userId, screenOperationId);
             return Mapper.Map<List<AdditionalOperationPermissionModel>>(additionalOperationPermissionList);
         }
    }
}
