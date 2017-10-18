using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;
using SecurityAdministration.BLL.ViewModels;

namespace SecurityAdministration.DAL.Repositories
{
    public class ApplicationRepository : GenericRepository<Application>
    {
        public ApplicationRepository(ERP_Security context): base(context)
        {


        }



        public int InactivateApplication()
        {
            return context.Database.ExecuteSqlCommand("UPDATE Application SET IsActive = 0");
        }


        //public IEnumerable<ApplicationView> GetAll()
        //{
        //    const string itemQuery = "SELECT ApplicationID,Title,IsActive,Description,FORMAT(SetOn,'dd-MMM-yyyy') as SetOn,SetBy" +
        //                             " FROM MAPPLE_DB.Security.Applications WHERE IsDelete = 0";

        //    var applicationList = context.Database.SqlQuery<ApplicationView>(itemQuery);
        //    return applicationList.ToList();
        //}

        //public ApplicationView GetByValue(byte? id)
        //{
        //    string itemQuery = "SELECT ApplicationID,Title,IsActive,Description,FORMAT(SetOn,'dd-MMM-yyyy') as SetOn,SetBy" +
        //                       " FROM MAPPLE_DB.Security.Applications WHERE ApplicationID = '" + id + "' AND IsDelete = 0";

        //    return context.Database.SqlQuery<ApplicationView>(itemQuery).FirstOrDefault();
        //}
        public IEnumerable<Application> GetByValue(int id)
        {
            var query = "SELECT * FROM [Security].[Applications] WHERE ApplicationID ='" + id + "'";
            return context.Database.SqlQuery<Application>(query, id);
        }


        public int IsDeleteTrue(byte? id)
        {
            var query = new StringBuilder();
            query.Append("UPDATE [Security].[Applications]");
            query.Append(" SET [IsDelete] = 1");
            query.Append(" WHERE [ApplicationID] = '" + id + "'");

            return context.Database.ExecuteSqlCommand(query.ToString());
        }



    }
}