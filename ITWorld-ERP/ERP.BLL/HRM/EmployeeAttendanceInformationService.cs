using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;
using AutoMapper;
using System;


namespace ERP.BLL.HRM
{

    public partial interface IEmployeeAttendanceInformationService : IBaseService<EmployeeAttendanceInformationModel, EmployeeAttendanceInformation>
    {
        void UpdatePunchOutTime(EmployeeAttendanceInformationModel employeeAttendanceInformation);
        List<EmployeeAttendanceInformationModel> GetEmployeeAttendanceInformationList(long employeeId, DateTime? day, long? month, long? year);
        List<EmployeeAttendanceInformationModel> GetEmployeeAttendanceInformationListWithDate(long employeeId, string date, long? month, long? year);
        EmployeeAttendanceInformationModel GetEmployeeAttendanceInformations(string employeeCode, DateTime date);
        List<SelectListItem> GetMonthList();
        List<SelectListItem> GetYearList();
    }

    public class EmployeeAttendanceInformationService : BaseService<EmployeeAttendanceInformationModel, EmployeeAttendanceInformation>, IEmployeeAttendanceInformationService
    {
        private readonly IEmployeeAttendanceInformationRepository _employeeAttendanceInformationRepository;

        public EmployeeAttendanceInformationService(IEmployeeAttendanceInformationRepository employeeAttendanceInformationRepository)
            : base(employeeAttendanceInformationRepository)
        {
            _employeeAttendanceInformationRepository = employeeAttendanceInformationRepository;
        }

        public void UpdatePunchOutTime(EmployeeAttendanceInformationModel employeeAttendanceInformation)
        {
            var attendanceInformation = Mapper.Map<EmployeeAttendanceInformation>(employeeAttendanceInformation);
            _employeeAttendanceInformationRepository.UpdatePunchOutTime(attendanceInformation);
        }

        public List<EmployeeAttendanceInformationModel> GetEmployeeAttendanceInformationList(long employeeId, DateTime? day, long? month, long? year)
        {
            var employeeAttendanceInformationList = _employeeAttendanceInformationRepository.GetEmployeeAttendanceInformationList(employeeId, day, month, year);
            return Mapper.Map<List<EmployeeAttendanceInformationModel>>(employeeAttendanceInformationList);
        }
        public List<EmployeeAttendanceInformationModel> GetEmployeeAttendanceInformationListWithDate(long employeeId, string date, long? month, long? year)
        {
            var employeeAttendanceInformationList = _employeeAttendanceInformationRepository.GetEmployeeAttendanceInformationListWithDate(employeeId, date, month, year);
            return Mapper.Map<List<EmployeeAttendanceInformationModel>>(employeeAttendanceInformationList);
        }

        public EmployeeAttendanceInformationModel GetEmployeeAttendanceInformations(string employeeCode, DateTime date)
        {
            var employeAttendanceInfo = _employeeAttendanceInformationRepository.GetEmployeeAttendanceInformations(employeeCode, date);
            return Mapper.Map<EmployeeAttendanceInformationModel>(employeAttendanceInfo);
        }

        public List<SelectListItem> GetMonthList()
        {

            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text= "January",
                    Value= "1" 
                },
                new SelectListItem
                {
                    Text= "February",
                    Value= "2" 
                },
                new SelectListItem
                {
                    Text= "March",
                    Value= "3"
                },
                new SelectListItem
                {
                    Text= "April",
                    Value= "4"
                },
                new SelectListItem
                {
                    Text= "May",
                    Value= "5" 
                },
                new SelectListItem
                {
                    Text= "June",
                    Value= "6" 
                },
                new SelectListItem
                {
                    Text= "July",
                    Value= "7" 
                },
                new SelectListItem
                {
                    Text= "August",
                    Value= "8" 
                },
                new SelectListItem
                {
                    Text= "September",
                    Value= "9" 
                },
                new SelectListItem
                {
                    Text= "October",
                    Value= "10" 
                },
                new SelectListItem
                {
                    Text= "November",
                    Value= "11" 
                },
                new SelectListItem
                {
                    Text= "December",
                    Value= "12" 
                }
            
            
            };
        
        }

        public List<SelectListItem> GetYearList()
        {

            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text= "2014",
                    Value= "2014" 
                },
                new SelectListItem
                {
                    Text= "2015",
                    Value= "2015" 
                },
                new SelectListItem
                {
                    Text= "2016",
                    Value= "2016"
                },
                new SelectListItem
                {
                    Text= "2017",
                    Value= "2017" 
                },
                new SelectListItem
                {
                    Text= "2018",
                    Value= "2018" 
                },
                new SelectListItem
                {
                    Text= "2019",
                    Value= "2019" 
                },
                new SelectListItem
                {
                    Text= "2020",
                    Value= "2020" 
                },
                new SelectListItem
                {
                    Text= "2021",
                    Value= "2021" 
                },
                new SelectListItem
                {
                    Text= "2022",
                    Value= "2022" 
                },
                new SelectListItem
                {
                    Text= "2023",
                    Value= "2023" 
                },
                new SelectListItem
                {
                    Text= "2024",
                    Value= "2024"
                },
                new SelectListItem
                {
                    Text= "2025",
                    Value= "2025" 
                },
                new SelectListItem
                {
                    Text= "2026",
                    Value= "2026" 
                },
                new SelectListItem
                {
                    Text= "2027",
                    Value= "2027" 
                },
                new SelectListItem
                {
                    Text= "2028",
                    Value= "2028" 
                },
                new SelectListItem
                {
                    Text= "2029",
                    Value= "2029" 
                },
                new SelectListItem
                {
                    Text= "2030",
                    Value= "2030" 
                }
            
            
            };
        
        }



    }
}