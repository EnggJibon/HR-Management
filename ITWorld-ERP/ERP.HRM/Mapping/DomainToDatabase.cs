using AutoMapper;
using ERP.DAL.HRM;
using ERP.HRM.Domain;

namespace ERP.HRM.Mapping
{
    public class DomainToDatabase:Profile
    {
        protected override void Configure()
        {
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<AddressModel, Address>();
            CreateMap<AnnualHolidayCategoryModel, AnnualHolidayCategory>();
            CreateMap<DepartmentModel, Department>();
            CreateMap<DesignationModel, Designation>();
            CreateMap<EmployeeModel, Employee>();
            CreateMap<EmployeeEducationalQualificationModel, EmployeeEducationalQualification>();
            CreateMap<EmployeeEmploymentHistoryModel, EmployeeEmploymentHistory>();
            CreateMap<EmployeeHealthInformationModel, EmployeeHealthInformation>();
            CreateMap<EmployeeCategoryModel, EmployeeCategory>();
            CreateMap<LeaveApprovalStatusModel, LeaveApprovalStatu>();
            CreateMap<LeavePolicyModel, LeavePolicy>();
            CreateMap<LeaveTypeModel, LeaveType>();
            CreateMap<PersonalInformationModel, PersonalInformation>();
            CreateMap<RosterInformationModel, RosterInformation>();
            CreateMap<WeekendCategoryModel, WeekendCategory>();
            CreateMap<WeekendModel, Weekend>();
            CreateMap<EmployeeNomineeInformationModel, EmployeeNomineeInformation>();
            CreateMap<EmployeeReferenceInformationModel, EmployeeReferenceInformation>();
            CreateMap<EmployeeLeaveInformationModel, EmployeeLeaveInformation>();
            CreateMap<EmployeeFamilyMemberModel, EmployeeFamilyMember>();
            CreateMap<AnnualHolidayModel, AnnualHoliday>();
            CreateMap<EmployeeLeaveRequestModel, EmployeeLeaveRequest>();
            CreateMap<LeaveRequestTypeModel, LeaveRequestType>();
            CreateMap<EmployeeAttendanceInformationModel, EmployeeAttendanceInformation>();
        }
    }
}
