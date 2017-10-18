using System;
using System.Collections.Generic;
using System.Text;
using SecurityAdministration.BLL;
using SecurityAdministration.BLL.ViewModels;

namespace SecurityAdministration.DAL.Repositories
{
    public class UserCredentialInformationRepository : GenericRepository<UserCredentialInformation>
    {
        public UserCredentialInformationRepository(ERP_Security context)
            : base(context)
        {
        }

        private int PerformAudit_CredentialInfo(UserCredentialInformation userCredentialInfo, char cmdFlag)
        {
            string whereClause = "WHERE UserID = '" + userCredentialInfo.UserID + "' AND LoginID = '" + userCredentialInfo.LoginID + "'";

            int noOfRowsAffected = new GenericRepository<UserCredentialInformationAudit>(context).InsertsIntoAuditTable("[Security].[UserCredentialInformation]", "[Security].[UserCredentialInformationAudit]", whereClause, cmdFlag);

            return noOfRowsAffected;
        }

        public IEnumerable<UserView> Get(int? userID, string loginID)
        {
            var query = new StringBuilder();

            query.Append("SELECT U.UserID, U.UserCode, U.UserType, U.Title, U.FirstName, U.MiddleName, U.LastName,");
            query.Append(" U.DesignationID, U.Email, U.Phone, U.Mobile, U.IsActive, U.SupervisorUserID, U.Description,");
            query.Append(" FORMAT(U.SetOn, 'dd-MMM-yyyy') AS SetOn, U.SetBy,");
            query.Append(" D.Name AS DesignationTitle,");
            query.Append(" UCI.LoginID, UCI.Password, FORMAT(UCI.LastPasswordChangedDate, 'dd-MMM-yyyy') AS LastPasswordChangedDate, UCI.IsPasswordAccepted, UCI.IsLocked,");
            query.Append(" FORMAT(UCI.SetOn, 'dd-MMM-yyyy') AS UCI_SetOn, UCI.SetBy AS UCI_SetBy");
            query.Append(" FROM Security.Users AS U LEFT JOIN");
            query.Append(" Security.Designations AS D ON U.DesignationID = D.DesignationID INNER JOIN");
            query.Append(" Security.UserCredentialInformation AS UCI ON U.UserID = UCI.UserID");
            query.Append(" WHERE 1=1");

            if (userID != null)
            {
                query.Append(" AND U.UserID = {0}");
            }
            if (!string.IsNullOrWhiteSpace(loginID))
            {
                query.Append(" AND UCI.LoginID = {1}");
            }

            return context.Database.SqlQuery<UserView>(query.ToString(), userID, loginID);
        }

        public void InsertUserCredentialInfo(UserCredentialInformation user)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO [Security].[UserCredentialInformation] ([UserID], [LoginID], [Password], [LastPasswordChangedDate], [IsPasswordAccepted], [IsLocked], [SetOn], [SetBy])");
            query.Append(" VALUES ('" + user.UserID + "'");
            query.Append(" ,'" + user.LoginID + "'");
            query.Append(" ,'" + user.Password + "'");
            query.Append(" ,'" + user.LastPasswordChangedDate + "'");
            query.Append(" ,'" + user.IsPasswordAccepted + "'");
            query.Append(" ,'" + user.IsLocked + "'");
            query.Append(" ,'" + user.SetOn + "'");
            query.Append(" ,'" + user.SetBy + "')");

            if (context.Database.ExecuteSqlCommand(query.ToString()) > 0)
            {
                PerformAudit_CredentialInfo(user, Convert.ToChar(AuditStatusFlag.Create));
            }
        }

        public void LockUser(UserCredentialInformation user)
        {
            user.IsLocked = true;

            var query = new StringBuilder();
            query.Append("UPDATE [Security].[Users]");
            query.Append(" SET [IsLocked] = '" + user.IsLocked + "'");
            query.Append(" ,[SetOn] = '" + user.SetOn + "'");
            query.Append(" ,[SetBy] = '" + user.SetBy + "'");
            query.Append(" WHERE [UserID] = {0}");

            if (context.Database.ExecuteSqlCommand(query.ToString(), user.UserID) > 0)
            {
                PerformAudit_CredentialInfo(user, Convert.ToChar(AuditStatusFlag.Modify));
            }
        }

        public int UpdatePassword(UserCredentialInformation userCredential)
        {
            var query = new StringBuilder();
            query.Append("UPDATE [Security].[UserCredentialInformation]");
            query.Append(" SET [Password] = '" + userCredential.Password + "'");
            query.Append(" ,[SetOn] = '" + userCredential.SetOn + "'");
            query.Append(" ,[SetBy] = '" + userCredential.SetBy + "'");
            query.Append(" WHERE [UserID] = {0}");

            return context.Database.ExecuteSqlCommand(query.ToString(), userCredential.UserID);
        }
    }
}