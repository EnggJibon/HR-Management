using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityAdministration.BLL.ViewModels;

namespace SecurityAdministration.DAL.Repositories
{
    public class ModuleRepository:GenericRepository<Module>
    {
        public ModuleRepository(ERP_Security context) : base(context)
        {
        }

        public IEnumerable<ModuleView> GetByValue(string moduleID)
        {
            var query = new StringBuilder();

            query.Append("SELECT ModuleID, ApplicationID, Title, IsActive, Description,");
            query.Append(" FORMAT(SetOn, 'dd-MMM-yyyy') AS SetOn, SetBy");
            query.Append(" FROM [Security].[Modules] WHERE ModuleID = " + moduleID);
            return context.Database.SqlQuery<ModuleView>(query.ToString(), moduleID);
        }

        public IEnumerable<ModuleView> GetAll()
        {
            const string query = "SELECT M.ModuleID, M.Title, M.IsActive, M.SetBy,FORMAT(M.SetOn, 'dd-MMM-yyyy') AS SetOn,AP.ApplicationID, AP.Title AS ApplicationTitle " +
                                 "FROM [Security].[Modules] m left join [Security].Applications AP on m.ApplicationID=AP.ApplicationID where M.IsDelete=0";
            return context.Database.SqlQuery<ModuleView>(query).ToList();
        }

        public int IsDeleteTrue(string id)
        {
            var query = new StringBuilder();
            query.Append("UPDATE [Security].[Modules]");
            query.Append(" SET [IsDelete] = 1");
            query.Append(" WHERE [ModuleID] = '" + id + "'");

            return context.Database.ExecuteSqlCommand(query.ToString()); 
        }

        public string GetLastModuleID()
        {
            return context.Modules.Max(m => m.ModuleID);
        }
    }
}