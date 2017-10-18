using System.Collections.Generic;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;
using System.Linq;

namespace ERP.DAL.Security.Repository
{

    public partial interface IScreenRepository : IBaseRepository<Screen>
    {
        List<USP_GetScreenList_Result> GetScreens(long? id, long? moduleId);
    }
    public class ScreenRepository : BaseRepository<Screen, ERP_Security>, IScreenRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();

        public ScreenRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<USP_GetScreenList_Result> GetScreens(long? id, long? moduleId)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetScreenList] ");
            query.Append("'" + id + "', ");
            query.Append("'" + moduleId + "'");

            string finalQuery = query.ToString().Contains("''") ? query.ToString().Replace("''", "NULL") : query.ToString();
            return _dbContextSecurity.Database.SqlQuery<USP_GetScreenList_Result>(finalQuery).ToList();
        }
    }
}
