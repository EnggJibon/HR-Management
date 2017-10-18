using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{

    public partial interface IRoleWiseOperationPermissionRepository : IBaseRepository<RoleWiseOperationPermission>
    {
        List<USP_GetRoleWiseOperationPermissionList_Result> GetRoleWiseOperationPermissions(long? id, long? roleId, long? screenOperationId);
    }

    public class RoleWiseOperationPermissionRepository : BaseRepository<RoleWiseOperationPermission, ERP_Security>, IRoleWiseOperationPermissionRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();

        public RoleWiseOperationPermissionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<USP_GetRoleWiseOperationPermissionList_Result> GetRoleWiseOperationPermissions(long? id, long? roleId, long? screenOperationId)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetRoleWiseOperationPermissionList] ");
            query.Append("'" + id + "', ");
            query.Append("'" + roleId + "', ");
            query.Append("'" + screenOperationId + "'");

            string finalQuery = query.ToString().Contains("''") ? query.ToString().Replace("''", "NULL") : query.ToString();
            return _dbContextSecurity.Database.SqlQuery<USP_GetRoleWiseOperationPermissionList_Result>(finalQuery).ToList();
        }
    }
}
