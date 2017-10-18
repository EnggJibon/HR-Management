using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityAdministration.BLL.ViewModels;

namespace SecurityAdministration.DAL.Repositories
{
    public class AdditionalScreenPermissionRepository : GenericRepository<AdditionalScreenPermission>
    {
        public AdditionalScreenPermissionRepository(ERP_Security context)
            : base(context)
        {
        }

        public bool IsExistAdditionalScreenPermission(int userID, string screenCode)
        {
            var query = new StringBuilder();
            query.Append("SELECT COUNT([UserID]) FROM Security.AdditionalScreenPermissions AS ASP WHERE ASP.UserID = {0} AND ASP.ScreenCode = {1}");
         
            return context.Database.SqlQuery<int>(query.ToString(), userID, screenCode).FirstOrDefault() > 0;
        }

        public IEnumerable<AdditionalScreenPermissionView> Get(int? userID = null, string moduleID = "", string screenCode = "")
        {
            var query = new StringBuilder();
            query.Append("SELECT ASP.UserID, U.UserType, U.Title, U.FirstName, U.MiddleName, U.LastName,");
            query.Append(" M.ModuleID, M.Title AS ModuleTitle, ASP.ScreenCode, S.Title AS ScreenTitle,");
            query.Append(" CASE WHEN SUBSTRING (ASP.AccessRight,1,1)='1' THEN 'Yes' ELSE 'No' END CanRead,");
            query.Append(" CASE WHEN SUBSTRING (ASP.AccessRight,2,1)='1' THEN 'Yes' ELSE 'No' END CanCreate,");
            query.Append(" CASE WHEN SUBSTRING (ASP.AccessRight,3,1)='1' THEN 'Yes' ELSE 'No' END CanUpdate,");
            query.Append(" CASE WHEN SUBSTRING (ASP.AccessRight,4,1)='1' THEN 'Yes' ELSE 'No' END CanDelete,");
            query.Append(" FORMAT(ASP.StartDate, 'dd-MMM-yyyy') AS StartDate,");
            query.Append(" FORMAT(ASP.EndDate, 'dd-MMM-yyyy') AS EndDate,");
            query.Append(" FORMAT(ASP.SetOn, 'dd-MMM-yyyy') AS SetOn, ASP.SetBy");
            query.Append(" FROM Security.AdditionalScreenPermissions AS ASP INNER JOIN Security.Users AS U ON ASP.UserID = U.UserID");
            query.Append(" INNER JOIN Security.Screens AS S ON ASP.ScreenCode = S.ScreenCode INNER JOIN Security.Modules AS M ON S.ModuleID = M.ModuleID");
            query.Append(" WHERE 1=1");
            if (userID != null)
            {
                query.Append(" AND ASP.UserID = {0}");
            }
            if (!string.IsNullOrWhiteSpace(moduleID))
            {
                query.Append(" AND S.ModuleID = {1}");
            }
            if (!string.IsNullOrWhiteSpace(screenCode))
            {
                query.Append(" AND ASP.ScreenCode = {2}");
            }
            query.Append(" ORDER BY ASP.SetOn DESC");

            return context.Database.SqlQuery<AdditionalScreenPermissionView>(query.ToString(), userID, moduleID, screenCode);
        }

        public int InsertAdditionalScreenPermission(AdditionalScreenPermission screenPermission)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO [Security].[AdditionalScreenPermissions] ([UserID], [ScreenCode], [AccessRight], [StartDate], [EndDate], [SetOn], [SetBy])");
            query.Append(" VALUES ('" + screenPermission.UserID + "'");
            query.Append(" ,'" + screenPermission.ScreenCode + "'");
            query.Append(" ,'" + screenPermission.AccessRight + "'");
            query.Append(" ,'" + screenPermission.StartDate + "'");
            query.Append(" ,'" + screenPermission.EndDate + "'");
            query.Append(" ,'" + screenPermission.SetOn + "'");
            query.Append(" ,'" + screenPermission.SetBy + "')");

            return context.Database.ExecuteSqlCommand(query.ToString());
        }

        public int UpdateAdditionalScreenPermission(AdditionalScreenPermission screenPermission)
        {
            var query = new StringBuilder();
            query.Append("UPDATE [Security].[AdditionalScreenPermissions]");
            query.Append(" SET [AccessRight] = '" + screenPermission.AccessRight + "'");
            query.Append(" ,[StartDate] = '" + screenPermission.StartDate + "'");
            query.Append(" ,[EndDate] = '" + screenPermission.EndDate + "'");
            query.Append(" ,[SetOn] = '" + screenPermission.SetOn + "'");
            query.Append(" ,[SetBy] = '" + screenPermission.SetBy + "'");
            query.Append(" WHERE [UserID] = '" + screenPermission.UserID + "' AND [ScreenCode] = '" + screenPermission.ScreenCode + "'");

            return context.Database.ExecuteSqlCommand(query.ToString());
        }

        public int DeleteAdditionalScreenPermission(int userID, string screenCode)
        {
            var query = new StringBuilder();
            query.Append("DELETE [Security].[AdditionalScreenPermissions]");
            query.Append(" WHERE [UserID] = {0} AND [ScreenCode] = {1}");

            return context.Database.ExecuteSqlCommand(query.ToString(), userID, screenCode);
        }
    }
}