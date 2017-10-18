using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IAdditionalOperationPermissionRepository : IBaseRepository<AdditionalOperationPermission>
    {
        List<USP_GetAdditionalOperationPermissionList_Result> GetRoleWiseOperationPermissions(long? id, long? userId, long? screenOperationId);
    }

    public class AdditionalOperationPermissionRepository : BaseRepository<AdditionalOperationPermission, ERP_Security>, IAdditionalOperationPermissionRepository
    {

        private readonly ERP_Security _dbContextSecurity = new ERP_Security();
        public AdditionalOperationPermissionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<USP_GetAdditionalOperationPermissionList_Result> GetRoleWiseOperationPermissions(long? id, long? userId, long? screenOperationId)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetAdditionalOperationPermissionList] ");
            query.Append("'" + id + "', ");
            query.Append("'" + userId + "', ");
            query.Append("'" + screenOperationId + "'");

            string finalQuery = query.ToString().Contains("''") ? query.ToString().Replace("''", "NULL") : query.ToString();
            return _dbContextSecurity.Database.SqlQuery<USP_GetAdditionalOperationPermissionList_Result>(finalQuery).ToList();
        }

    }
}


