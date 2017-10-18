using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{

    public partial interface IRoleWiseScreenPermissionRepository : IBaseRepository<RoleWiseScreenPermission>
    {
        List<RoleWiseScreenPermission> GetRoleWiseScreenList(long roleId);
        List<USP_GetRoleWiseScreenPermissionList_Result> GetRoleWiseScreenPermissions(long? id, long? roleId, long? moduleId, long? screenId);
    }

    public class RoleWiseScreenPermissionRepository : BaseRepository<RoleWiseScreenPermission, ERP_Security>, IRoleWiseScreenPermissionRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();

        public RoleWiseScreenPermissionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<RoleWiseScreenPermission> GetRoleWiseScreenList(long roleId)
        {
            return _dbContextSecurity.RoleWiseScreenPermissions.Where(s => s.RoleId == roleId).ToList();
        }

        public List<USP_GetRoleWiseScreenPermissionList_Result> GetRoleWiseScreenPermissions(long? id, long? roleId, long? moduleId, long? screenId)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetRoleWiseScreenPermissionList] ");
            query.Append("'" + id + "', '" + roleId + "', ");
            query.Append("'" + moduleId + "', '" + screenId + "'");

            string finalQuery = query.ToString().Contains("''")
                ? query.ToString().Replace("''", "NULL")
                : query.ToString();

            return _dbContextSecurity.Database.SqlQuery<USP_GetRoleWiseScreenPermissionList_Result>(finalQuery).ToList();
        }
    }
}
