using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class EmployeeLeaveRequestController : Controller
    {
        private readonly IEmployeeLeaveRequestService _employeeLeaveRequestService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveApprovalStatusService _leaveApprovalStatusService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeLeaveInformationService _employeeLeaveInformationService;

        public EmployeeLeaveRequestController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _employeeLeaveRequestService = kernel.GetService(typeof(EmployeeLeaveRequestService)) as EmployeeLeaveRequestService;
                _leaveTypeService = kernel.GetService(typeof(ILeaveTypeService)) as LeaveTypeService;
                _leaveApprovalStatusService = kernel.GetService(typeof(ILeaveApprovalStatusService)) as LeaveApprovalStatusService;
                _employeeService = kernel.GetService(typeof(IEmployeeService)) as EmployeeService;
                _employeeLeaveInformationService = kernel.GetService(typeof(IEmployeeLeaveInformationService)) as IEmployeeLeaveInformationService;
            }
        }

        public ActionResult Index()
        {
            var employeeLeaveRequestView = new EmployeeLeaveRequestViewModel
            {
                LeaveTypeList = new SelectList(_leaveTypeService.GetAll(), "Id", "Name"),
                LeaveApprovalStatusList = new SelectList(_leaveApprovalStatusService.GetAll(), "Id", "Name"),
            };
            return View(employeeLeaveRequestView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            if (employeeId <= 0)
            {
                employeeId = LoginInformation.UserInformation.EmployeeId;
            }

            var leaveRequestList = _employeeLeaveRequestService.GetAll().Where(w => w.EmployeeId == employeeId).ToList();
            foreach (var leaveRequest in leaveRequestList)
            {
                leaveRequest.ApprovalStatusName = _leaveApprovalStatusService.GetById(leaveRequest.ApprovalStatusId).Name;
            }
            return Json(leaveRequestList.ToDataSourceResult(request));
        }

        public JsonResult GetByEmployeeCode(string employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode))
            {
                employeeCode = LoginInformation.UserInformation.EmployeeCode;
            }

            var employeeInfo = _employeeService.GetEmployeeInformation(null, employeeCode);
            return new JsonResult { Data = employeeInfo };
        }

        public JsonResult GetByEmployeeId(long employeeId)
        {
            if (employeeId <= 0)
            {
                employeeId = LoginInformation.UserInformation.EmployeeId;
            }

            var employeeInfo = _employeeService.GetEmployeeInformation(employeeId, null);
            return new JsonResult { Data = employeeInfo };
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save(EmployeeLeaveRequestModel employeeLeaveRequest, bool isInsert)
        {
            var leaveBalanceInformation = _employeeLeaveInformationService.GetEmployeeLeaveBalanceInformation(employeeLeaveRequest.EmployeeId);

            if (leaveBalanceInformation.Any())
            {
                var requestedLeaveTypeBalance = leaveBalanceInformation.FirstOrDefault(s => s.LeaveTypeId == employeeLeaveRequest.LeaveTypeId);
                if (requestedLeaveTypeBalance != null && requestedLeaveTypeBalance.RemainingDays > 0)
                {
                    employeeLeaveRequest.RequestDate = DateTime.Now;
                    if (isInsert)
                    {
                        employeeLeaveRequest.ApprovalStatusId = 1;

                        employeeLeaveRequest.SetCreateProperties(LoginInformation.UserInformation.Id);
                        employeeLeaveRequest.Id = _employeeLeaveRequestService.Insert(employeeLeaveRequest);
                    }
                    else
                    {
                        employeeLeaveRequest.SetUpdateProperties(LoginInformation.UserInformation.Id);
                        _employeeLeaveRequestService.Update(employeeLeaveRequest);
                    }
                    return Json(new { Success = true, Data = employeeLeaveRequest });
                }
            }
            return Json(new { Success = false, Message = "You do not have sufficient leave balance of requested type." });
        }

        public JsonResult Approve(EmployeeLeaveRequestModel employeeLeaveRequest)
        {
            var leaveBalanceInformation = _employeeLeaveInformationService.GetEmployeeLeaveBalanceInformation(employeeLeaveRequest.EmployeeId)
                .FirstOrDefault(s => s.LeaveTypeId == employeeLeaveRequest.LeaveTypeId);

            if (leaveBalanceInformation == null)
            {
                return Json(new { Success = false, Message = "Requester leave information is not found." });
            }

            var leaveRequestInformationFromDB = _employeeLeaveRequestService.GetById(employeeLeaveRequest.Id);

            if (leaveRequestInformationFromDB.ApprovalStatusId == 3 && employeeLeaveRequest.ApprovalStatusId != 3)
            {
                return Json(new { Success = false, Message = "Request is already been approved. It's status cannot be changed." });
            }

            var employeeInformation = _employeeService.GetById(employeeLeaveRequest.EmployeeId);

            if (employeeLeaveRequest.ApprovalStatusId == 2)
            {
                if (employeeInformation.IsDualApprovalApplied &&
                    (employeeInformation.SupervisorId != LoginInformation.UserInformation.EmployeeId
                    || employeeInformation.ApproverId != LoginInformation.UserInformation.EmployeeId))
                {
                    return Json(new { Success = false, Message = "You do not have any permission to approve the request." });
                }

                if (!(leaveRequestInformationFromDB.ApprovalStatusId == 1 || leaveRequestInformationFromDB.ApprovalStatusId == 4))
                {
                    return Json(new { Success = false, Message = "You can recommend only that requests which are in 'Requested' or 'Rejected' status." });
                }
            }

            if (employeeLeaveRequest.ApprovalStatusId == 3)
            {
                if (employeeInformation.ApproverId != LoginInformation.UserInformation.EmployeeId)
                {
                    return Json(new { Success = false, Message = "You do not have any permission to approve the request." });
                }

                if (employeeInformation.IsDualApprovalApplied && leaveRequestInformationFromDB.ApprovalStatusId != 2)
                {
                    return Json(new { Success = false, Message = "You can approve only that requests which are in 'Recommended' status." });
                }
            }

            if (leaveBalanceInformation.RemainingDays < employeeLeaveRequest.DaysCount)
            {
                return Json(new { Success = false, Message = "Requester does not have sufficient leave balance for the requested type." });
            }

            employeeLeaveRequest.ApprovalDate = DateTime.Now;
            _employeeLeaveRequestService.UpdateApprovalStatus(employeeLeaveRequest);
            return Json(new { Success = true, Data = employeeLeaveRequest });
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _employeeLeaveRequestService.GetById(id) };
        }

        public void Delete(long id)
        {
            _employeeLeaveRequestService.DeleteSoftly(id);
        }

        public ActionResult IncomingLeaveRequest()
        {
            var employeeLeaveRequestView = new EmployeeLeaveRequestViewModel
            {
                LeaveTypeList = new SelectList(_leaveTypeService.GetAll(), "Id", "Name"),
                LeaveApprovalStatusList = new SelectList(_leaveApprovalStatusService.GetAll(), "Id", "Name"),
            };
            return View(employeeLeaveRequestView);
        }

        public ActionResult IncomingRequestList([DataSourceRequest]DataSourceRequest request)
        {
            var leaveRequestList = _employeeLeaveRequestService.GetEmployeeLeaveRequests(LoginInformation.UserInformation.EmployeeId);
            LoginInformation.IncomingLeaveRequest = leaveRequestList.Where(s => s.ApprovalStatusId == 1 || s.ApprovalStatusId == 2).ToList().Count;

            foreach (var leaveRequest in leaveRequestList)
            {
                leaveRequest.ApprovalStatusName = _leaveApprovalStatusService.GetById(leaveRequest.ApprovalStatusId).Name;
            }
            return Json(leaveRequestList.ToDataSourceResult(request));
        }
    }
}