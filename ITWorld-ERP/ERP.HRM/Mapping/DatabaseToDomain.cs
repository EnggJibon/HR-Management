using AutoMapper;
using ERP.DAL.HRM;
using ERP.HRM.Domain;

namespace ERP.HRM.Mapping
{
    public class DatabaseToDomain : Profile
    {
        protected override void Configure()
        {
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Address, AddressModel>();
            CreateMap<AnnualHoliday, AnnualHolidayModel>();
            CreateMap<AnnualHolidayCategory, AnnualHolidayCategoryModel>();
            CreateMap<Department, DepartmentModel>();
            CreateMap<Designation, DesignationModel>();
            CreateMap<Employee, EmployeeModel>();
            CreateMap<EmployeeAttendanceInformation, EmployeeAttendanceInformationModel>();
            CreateMap<EmployeeCategory, EmployeeCategoryModel>();
            CreateMap<EmployeeEducationalQualification,EmployeeEducationalQualificationModel>();
            CreateMap<EmployeeEmploymentHistory, EmployeeEmploymentHistoryModel>();
            CreateMap<EmployeeFamilyMember, EmployeeFamilyMemberModel>();
            CreateMap<EmployeeHealthInformation, EmployeeHealthInformationModel>();
            CreateMap<EmployeeLeaveInformation, EmployeeLeaveInformationModel>();
            CreateMap<EmployeeLeaveRequest, EmployeeLeaveRequestModel>();
            CreateMap<EmployeeNomineeInformation, EmployeeNomineeInformationModel>();
            CreateMap<EmployeeReferenceInformation, EmployeeReferenceInformationModel>();
            CreateMap<LeaveApprovalStatu, LeaveApprovalStatusModel>();
            CreateMap<LeavePolicy, LeavePolicyModel>();
            CreateMap<LeaveRequestType, LeaveRequestTypeModel>();
            CreateMap<LeaveType, LeaveTypeModel>();
            CreateMap<PersonalInformation, PersonalInformationModel>();
            CreateMap<RosterInformation, RosterInformationModel>();
            CreateMap<Weekend, WeekendModel>();
            CreateMap<WeekendCategory, WeekendCategoryModel>();
            
            CreateMap<USP_GetEmployees_Result, EmployeeModel>();
            CreateMap<USP_GetEmployeeDetails_Result, EmployeeModel>();
            CreateMap<USP_GetEmployeeAttendanceInformationList_Result, EmployeeAttendanceInformationModel>();
            CreateMap<USP_GetEmployeeLeaveBalanceInformation_Result, EmployeeLeaveBalanceInformation>();
        }
    }
}
