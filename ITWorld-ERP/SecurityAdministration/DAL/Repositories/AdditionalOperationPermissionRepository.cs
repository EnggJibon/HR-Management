using System.Collections.Generic;
using System.Text;

namespace SecurityAdministration.DAL.Repositories
{
    public class AdditionalOperationPermissionRepository : GenericRepository<AdditionalOperationPermission>
    {
        public AdditionalOperationPermissionRepository(ERP_Security context)
            : base(context)
        {
        }

        public IEnumerable<AdditionalOperationPermission> Get(int? userID = null, string operationID = "", string screenCode = "")
        {
            var query = new StringBuilder();
            query.Append("SELECT AOP.UserID, U.Title, U.FirstName, U.MiddleName, U.LastName, U.UserType, AOP.OperationID, SO.OperationTitle,");
            query.Append(" AOP.HaveAccess, AOP.StartDate, AOP.EndDate, AOP.SetOn, AOP.SetBy, U.DesignationID, D.Name AS DesignationName,");
            query.Append(" SO.ScreenCode, S.Title AS ScreenTitle, S.ModuleID, M.Title AS ModuleTitle");
            query.Append(" FROM Security.Screens AS S INNER JOIN");
            query.Append(" Security.ScreenOperations AS SO ON S.ScreenCode = SO.ScreenCode INNER JOIN");
            query.Append(" Security.Modules AS M ON S.ModuleID = M.ModuleID INNER JOIN");
            query.Append(" Security.AdditionalOperationPermissions  AS AOP ON SO.OperationID = AOP.OperationID INNER JOIN");
            query.Append(" Security.Designations AS D INNER JOIN");
            query.Append(" Security.Users AS U ON D.DesignationID = U.DesignationID ON AOP.UserID = U.UserID");
            query.Append(" WHERE 1=1");
            if (userID != null)
            {
                query.Append(" AND AOP.UserID = {0}");
            }
            if (!string.IsNullOrWhiteSpace(operationID))
            {
                query.Append(" AOP.OperationID = {1}");
            }
            if (!string.IsNullOrWhiteSpace(screenCode))
            {
                query.Append(" AND SO.ScreenCode = {2}");
            }
            query.Append(" ORDER BY ASP.SetOn DESC");

            return context.Database.SqlQuery<AdditionalOperationPermission>(query.ToString(), userID, operationID, screenCode);
        }
    }
}