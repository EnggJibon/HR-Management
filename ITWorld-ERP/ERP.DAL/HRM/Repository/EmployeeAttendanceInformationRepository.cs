using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;
using System;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IEmployeeAttendanceInformationRepository : IBaseRepository<EmployeeAttendanceInformation>
    {
        void UpdatePunchOutTime(EmployeeAttendanceInformation employeeAttendanceInformation);
        List<USP_GetEmployeeAttendanceInformationList_Result> GetEmployeeAttendanceInformationList(long employeeId, DateTime? day, long? month, long? year);

        List<USP_GetEmployeeAttendanceInformationList_Result> GetEmployeeAttendanceInformationListWithDate(long employeeId, string date, long? month, long? year);
        EmployeeAttendanceInformation GetEmployeeAttendanceInformations(string employeeCode, DateTime date);
    }

    public class EmployeeAttendanceInformationRepository : BaseRepository<EmployeeAttendanceInformation, ERP_HRM>, IEmployeeAttendanceInformationRepository
    {
        private readonly ERP_HRM _dbContextHRM = new ERP_HRM();

        public EmployeeAttendanceInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<EmployeeAttendanceInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }

        public void UpdatePunchOutTime(EmployeeAttendanceInformation employeeAttendanceInformation)
        {
            //var attendanceInfo = _dbContextHRM.EmployeeAttendanceInformations.FirstOrDefault(
            //                        w => w.EmployeeId == employeeAttendanceInformation.EmployeeId
            //                        && DbFunctions.TruncateTime(w.Date) == employeeAttendanceInformation.Date.Date);

            //if (attendanceInfo != null)
            //{
            //    attendanceInfo.PunchOutTime = employeeAttendanceInformation.PunchOutTime;
            //}

            const string command = "UPDATE [HRM].EmployeeAttendanceInformation SET PunchOutTime = {0} WHERE EmployeeId = {1} AND Date = {2}";
            _dbContextHRM.Database.ExecuteSqlCommand(command, employeeAttendanceInformation.PunchOutTime, employeeAttendanceInformation.EmployeeId, employeeAttendanceInformation.Date);
            _dbContextHRM.SaveChanges();
        }

        public List<USP_GetEmployeeAttendanceInformationList_Result> GetEmployeeAttendanceInformationList(long employeeId, DateTime? date, long? month, long? year)
        {
            var query = new StringBuilder();
            query.Append("EXEC [HRM].[USP_GetEmployeeAttendanceInformationList] ");
            query.Append("'" + employeeId + "', ");
            query.Append("'" + date + "', ");
            query.Append("'" + month + "',");
            query.Append("'" + year + "'");

            string finalQuery = query.ToString().Contains("''") ? query.ToString().Replace("''", "NULL") : query.ToString();
            return _dbContextHRM.Database.SqlQuery<USP_GetEmployeeAttendanceInformationList_Result>(finalQuery).ToList();
        }


        public List<USP_GetEmployeeAttendanceInformationList_Result> GetEmployeeAttendanceInformationListWithDate(long employeeId, string date, long? month, long? year)
        {
            var query = new StringBuilder();
            query.Append("EXEC [HRM].[USP_GetEmployeeAttendanceInformationList] ");
            query.Append("'" + employeeId + "', ");
            query.Append("'" + date + "', ");
            query.Append("'" + month + "',");
            query.Append("'" + year + "'");

            string finalQuery = query.ToString().Contains("''") ? query.ToString().Replace("''", "NULL") : query.ToString();
            return _dbContextHRM.Database.SqlQuery<USP_GetEmployeeAttendanceInformationList_Result>(finalQuery).ToList();
        }

        public EmployeeAttendanceInformation GetEmployeeAttendanceInformations(string employeeCode, DateTime date)
        {
            var attendanceInfo = (from eai in _dbContextHRM.EmployeeAttendanceInformations
                                  join e in _dbContextHRM.Employees on eai.EmployeeId equals e.Id
                                  where e.EmployeeCode == employeeCode && DbFunctions.TruncateTime(eai.Date) == date.Date
                                  select eai).FirstOrDefault();

            return attendanceInfo;
        }
    }
}
