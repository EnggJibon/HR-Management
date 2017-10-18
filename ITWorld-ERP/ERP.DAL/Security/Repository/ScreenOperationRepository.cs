using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IScreenOperationRepository : IBaseRepository<ScreenOperation>
    {
        List<USP_GetScreenOperationList_Result> GetScreenOperationDetails(long? id, long? screenId);
    }

    public class ScreenOperationRepository : BaseRepository<ScreenOperation, ERP_Security>, IScreenOperationRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();

        public ScreenOperationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<USP_GetScreenOperationList_Result> GetScreenOperationDetails(long? id, long? screenId)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetScreenOperationList] ");
            query.Append("'" + id + "', ");
            query.Append("'" + screenId + "'");

            string finalQuery = query.ToString().Contains("''") ? query.ToString().Replace("''", "NULL") : query.ToString();
            return _dbContextSecurity.Database.SqlQuery<USP_GetScreenOperationList_Result>(finalQuery).ToList();
        }

    }
}
