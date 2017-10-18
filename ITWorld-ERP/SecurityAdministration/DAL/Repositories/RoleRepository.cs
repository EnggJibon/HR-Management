using System.Collections.Generic;
using System.Text;
using SecurityAdministration.BLL.ViewModels;

namespace SecurityAdministration.DAL.Repositories
{
    public class RoleRepository:GenericRepository<Role>
    {
        public RoleRepository(ERP_Security context) : base(context)
        {
        }

        public int IsDeleteTrue(int? id)
        {
            var query = new StringBuilder();
            query.Append("UPDATE [Security].[Roles]");
            query.Append(" SET [IsDelete] = 1");
            query.Append(" WHERE [RoleID] = '" + id + "'");

            return context.Database.ExecuteSqlCommand(query.ToString());
        }
        public IEnumerable<RoleView> GetByValue(int id)
        {
            var query = "SELECT * FROM [Security].[Designations] WHERE DesinationID ='" + id + "'";
            return context.Database.SqlQuery<RoleView>(query, id);
        }
    }
}