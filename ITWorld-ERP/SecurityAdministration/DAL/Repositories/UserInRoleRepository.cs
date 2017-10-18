using System.Collections.Generic;
using System.Text;
using SecurityAdministration.BLL.ViewModels;

namespace SecurityAdministration.DAL.Repositories
{
    public class UserInRoleRepository : GenericRepository<UserInRole>
    {
        public UserInRoleRepository(ERP_Security context)
            : base(context)
        {
        }

        public IEnumerable<UserInRoleView> GetAll(int? userID = null, int? roleID = null)
        {
            var query = new StringBuilder();
            query.Append("SELECT UIR.UserID, CONCAT(U.Title, ' ', U.FirstName, ' ', U.MiddleName, ' ', U.LastName) AS UserFullName, D.Name AS DesignationName,");
            query.Append(" UIR.RoleID, R.RoleName, UIR.IsActive, UIR.SetOn, UIR.SetBy");
            query.Append(" FROM Security.Roles AS R INNER JOIN");
            query.Append(" Security.UserInRoles AS UIR ON R.RoleID = UIR.RoleID INNER JOIN");
            query.Append(" Security.Users AS U ON UIR.UserID = U.UserID LEFT JOIN");
            query.Append(" Security.Designations AS D ON D.DesignationID = U.DesignationID");
            query.Append(" WHERE 1=1");
            if (userID != null)
            {
                query.Append(" AND UIR.UserID = {0}");
            }
            if (roleID != null)
            {
                query.Append(" AND UIR.RoleID = {1}");
            }
            query.Append(" ORDER BY UIR.UserID");
            return context.Database.SqlQuery<UserInRoleView>(query.ToString(), userID, roleID);
        }

        public IEnumerable<UserInRole> Get(int userID, int? roleID)
        {
            var query = new StringBuilder();

            query.Append("SELECT [UserID], [RoleID], [IsActive], [SetOn], [SetBy] FROM [Security].[UserInRoles]");
            query.Append(" WHERE 1=1");

            if (userID > 0)
            {
                query.Append(" AND [UserID] = {0}");
            }
            if (roleID != null)
            {
                query.Append(" AND [RoleID] = {1}");
            }

            return GetWithRawSql(query.ToString(), userID, roleID);
        }

        public int InsertUserInRole(UserInRole userInRole)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO [Security].[UserInRoles] ([UserID], [RoleID], [IsActive], [SetOn], [SetBy]) VALUES (");
            query.Append("'" + userInRole.UserID + "'");
            query.Append(", '" + userInRole.RoleID + "'");
            query.Append(", '" + userInRole.IsActive + "'");
            query.Append(", '" + userInRole.SetOn + "'");
            query.Append(", '" + userInRole.SetBy + "')");

            return context.Database.ExecuteSqlCommand(query.ToString());
        }

        public int UpdateUserInRole(UserInRole userInRole)
        {
            var query = new StringBuilder();
            query.Append("UPDATE [Security].[UserInRoles]");
            query.Append(" SET [IsActive] = '" + userInRole.IsActive + "'");
            query.Append(" ,[SetOn] = '" + userInRole.SetOn + "'");
            query.Append(" ,[SetBy] = '" + userInRole.SetBy + "'");
            query.Append(" WHERE [UserID] = '" + userInRole.UserID + "' AND [RoleID] = '" + userInRole.RoleID + "'");

            return context.Database.ExecuteSqlCommand(query.ToString());
        }

        public int DeleteUserInRole(int userId, int roleId)
        {
            var query = new StringBuilder();
            query.Append("DELETE FROM [Security].[UserInRoles] WHERE [UserID]=");
            query.Append("'" + userId + "' AND [RoleID]=");
            query.Append("'" + roleId + "'");
            return context.Database.ExecuteSqlCommand(query.ToString());
        }

        public IEnumerable<UserInRoleView> CheckUser(int userID)
        {
            var query = new StringBuilder();

            query.Append("SELECT [UserID], [RoleID], [IsActive], [SetOn], [SetBy] FROM [Security].[UserInRoles]");
            query.Append(" WHERE 1=1");

            if (userID > 0)
            {
                query.Append(" AND [UserID] = {0}");
            }
            return context.Database.SqlQuery<UserInRoleView>(query.ToString(), userID);
        }
    }
}