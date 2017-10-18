using ERP.DAL.HRM.Repository;
using Ninject.Modules;

namespace ERP.BLL.HRM.IOC
{
    public partial class HRMRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAddressRepository>().To<AddressRepository>();
            Bind<IAnnualHolidayCategoryRepository>().To<AnnualHolidayCategoryRepository>();
            Bind<IDepartmentRepository>().To<DepartmentRepository>();
            Bind<IDesignationRepository>().To<DesignationRepository>();
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IEmployeeCategoryRepository>().To<EmployeeCategoryRepository>();
            Bind<IEmployeeEducationalQualificationRepository>().To<EmployeeEducationalQualificationRepository>();
            Bind<IEmployeeEmploymentHistoryRepository>().To<EmployeeEmploymentHistoryRepository>();
            Bind<IEmployeeHealthInformationRepository>().To<EmployeeHealthInformationRepository>();
            Bind<ILeaveApprovalStatusRepository>().To<LeaveApprovalStatusRepository>();
            Bind<ILeavePolicyRepository>().To<LeavePolicyRepository>();
            Bind<ILeaveTypeRepository>().To<LeaveTypeRepository>();
            Bind<IPersonalInformationRepository>().To<PersonalInformationRepository>();
            Bind<IRosterInformationRepository>().To<RosterInformationRepository>();
            Bind<IWeekendCategoryRepository>().To<WeekendCategoryRepository>();
            Bind<IWeekendRepository>().To<WeekendRepository>();
            Bind<IEmployeeLeaveInformationRepository>().To<EmployeeLeaveInformationRepository>();
            Bind<IEmployeeNomineeInformationRepository>().To<EmployeeNomineeInformationRepository>();
            Bind<IEmployeeReferenceInformationRepository>().To<EmployeeReferenceInformationRepository>();
            Bind<IEmployeeFamilyMemberRepository>().To<EmployeeFamilyMemberRepository>();
            Bind<ILeaveRequestTypeRepository>().To<LeaveRequestTypeRepository>();
            Bind<IEmployeeLeaveRequestRepository>().To<EmployeeLeaveRequestRepository>();
            Bind<IAnnualHolidayRepository>().To<AnnualHolidayRepository>();
            Bind<IEmployeeAttendanceInformationRepository>().To<EmployeeAttendanceInformationRepository>();
        }
    }
}

