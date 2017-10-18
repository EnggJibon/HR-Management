using Ninject.Modules;

namespace ERP.BLL.HRM.IOC 
{
	public partial class HRMServiceModule : NinjectModule
	{
		public override void Load()
        {
            Bind<IAddressService>().To<AddressService>();
            Bind<IAnnualHolidayCategoryService>().To<AnnualHolidayCategoryService>();
            Bind<IDepartmentService>().To<DepartmentService>();
            Bind<IDesignationService>().To<DesignationService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IEmployeeCategoryService>().To<EmployeeCategoryService>();
            Bind<IEmployeeEducationalQualificationService>().To<EmployeeEducationalQualificationService>();
            Bind<IEmployeeEmploymentHistoryService>().To<EmployeeEmploymentHistoryService>();
            Bind<IEmployeeHealthInformationService>().To<EmployeeHealthInformationService>();
            Bind<ILeaveApprovalStatusService>().To<LeaveApprovalStatusService>();
            Bind<ILeavePolicyService>().To<LeavePolicyService>();
            Bind<ILeaveTypeService>().To<LeaveTypeService>();
            Bind<IPersonalInformationService>().To<PersonalInformationService>();
            Bind<IRosterInformationService>().To<RosterInformationService>();
            Bind<IWeekendCategoryService>().To<WeekendCategoryService>();
            Bind<IWeekendService>().To<WeekendService>();
            Bind<IEmployeeNomineeInformationService>().To<EmployeeNomineeInformationService>();
            Bind<IEmployeeReferenceInformationService>().To<EmployeeReferenceInformationService>();
            Bind<IEmployeeLeaveInformationService>().To<EmployeeLeaveInformationService>();
            Bind<IEmployeeFamilyMemberService>().To<EmployeeFamilyMemberService>();
            Bind<ILeaveRequestTypeService>().To<LeaveRequestTypeService>();
            Bind<IEmployeeLeaveRequestService>().To<EmployeeLeaveRequestService>();
            Bind<IAnnualHolidayService>().To<AnnualHolidayService>();
            Bind<IEmployeeAttendanceInformationService>().To<EmployeeAttendanceInformationService>();
		}
	}
}

