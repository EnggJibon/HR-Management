using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IAdditionalScreenPermissionRepository : IBaseRepository<AdditionalScreenPermission>
    {
        List<USP_GetAdditionalScreenPermissionList_Result> GetAdditionalScreenPermissions(long? id, long? userId, long? moduleId, long? screenId);
    }

    public class AdditionalScreenPermissionRepository : BaseRepository<AdditionalScreenPermission, ERP_Security>, IAdditionalScreenPermissionRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();
        public AdditionalScreenPermissionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<USP_GetAdditionalScreenPermissionList_Result> GetAdditionalScreenPermissions(long? id, long? userId, long? moduleId, long? screenId)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetAdditionalScreenPermissionList] ");
            query.Append("'" + id + "', '" + userId + "', ");
            query.Append("'" + moduleId + "', '" + screenId + "'");

            string finalQuery = query.ToString().Contains("''")
                ? query.ToString().Replace("''", "NULL")
                : query.ToString();

            return _dbContextSecurity.Database.SqlQuery<USP_GetAdditionalScreenPermissionList_Result>(finalQuery).ToList();
        }
    }
}





