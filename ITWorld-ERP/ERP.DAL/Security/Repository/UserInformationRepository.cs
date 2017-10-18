using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IUserInformationRepository : IBaseRepository<UserInformation>
    {
        List<USP_GetUsers_Result> GetAllUsers();
        USP_GetUserDetails_Result GetUserDetails(long? id, string username);
        UserInformation GetUser(long employeeId);
        void UpdateUser(UserInformation userInformation);
        void UpdateUserPassword(UserInformation userInformation);
    }

    public class UserInformationRepository : BaseRepository<UserInformation, ERP_Security>, IUserInformationRepository
    {
        private readonly ERP_Security _dbContextSecurity = new ERP_Security();

        public UserInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<USP_GetUsers_Result> GetAllUsers()
        {
            return _dbContextSecurity.Database.SqlQuery<USP_GetUsers_Result>("EXEC [Security].[USP_GetUsers]").ToList();
        }

        public USP_GetUserDetails_Result GetUserDetails(long? id, string username)
        {
            var query = new StringBuilder();
            query.Append("EXEC [Security].[USP_GetUserDetails] ");
            query.Append("'" + id + "', '" + username + "'");

            string finalQuery = query.ToString().Contains("''")
                ? query.ToString().Replace("''", "NULL")
                : query.ToString();

            return _dbContextSecurity.Database.SqlQuery<USP_GetUserDetails_Result>(finalQuery).FirstOrDefault(user => user.Id == id || user.Username == username);
        }

        public UserInformation GetUser(long employeeId)
        {
            return _dbContextSecurity.UserInformations.FirstOrDefault(s => s.EmployeeId == employeeId);
        }

        public void UpdateUser(UserInformation userInformation)
        {
            _dbContextSecurity.Entry(userInformation).State = EntityState.Modified;
            _dbContextSecurity.Entry(userInformation).Property(x => x.Password).IsModified = false;
            _dbContextSecurity.SaveChanges();
        }

        public void UpdateUserPassword(UserInformation userInformation)
        {
            _dbContextSecurity.UserInformations.Attach(userInformation);
            _dbContextSecurity.Entry(userInformation).Property(x => x.Password).IsModified = true;
            _dbContextSecurity.Entry(userInformation).Property(x => x.IsPasswordChanged).IsModified = true;
            _dbContextSecurity.Entry(userInformation).Property(x => x.ModifiedOn).IsModified = true;
            _dbContextSecurity.Entry(userInformation).Property(x => x.ModifiedBy).IsModified = true;
            _dbContextSecurity.SaveChanges();
        }
    }
}
