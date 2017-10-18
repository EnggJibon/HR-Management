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
    public partial interface IAdditionalScreenPermissionService : IBaseService<AdditionalScreenPermissionModel, AdditionalScreenPermission>
    {
        List<AdditionalScreenPermissionModel> GetAdditionalScreenPermissionList(long? id, long? userId, long? moduleId, long? screenId);
        AdditionalScreenPermissionModel GetAdditionalScreenPermissionDetails(long id);
    }

    public class AdditionalScreenPermissionService : BaseService<AdditionalScreenPermissionModel, AdditionalScreenPermission>, IAdditionalScreenPermissionService
    {
        private readonly IAdditionalScreenPermissionRepository _additionalScreenPermissionRepository;

        public AdditionalScreenPermissionService(IAdditionalScreenPermissionRepository additionalScreenPermissionRepository)
            : base(additionalScreenPermissionRepository)
        {
            _additionalScreenPermissionRepository = additionalScreenPermissionRepository;
        }

        public List<AdditionalScreenPermissionModel> GetAdditionalScreenPermissionList(long? id, long? userId, long? moduleId, long? screenId)
        {
            var additionalScreenPermissionList = _additionalScreenPermissionRepository.GetAdditionalScreenPermissions(id, userId, moduleId, screenId);
            return Mapper.Map<List<AdditionalScreenPermissionModel>>(additionalScreenPermissionList);
        }

        public AdditionalScreenPermissionModel GetAdditionalScreenPermissionDetails(long id)
        {
            var screenPermission = _additionalScreenPermissionRepository.GetAdditionalScreenPermissions(id, null, null, null).FirstOrDefault();
            return Mapper.Map<AdditionalScreenPermissionModel>(screenPermission);
        }
    }
}


