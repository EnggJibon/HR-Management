using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class LeaveTypeController : Controller
    {
        private readonly LeaveTypeService _leaveTypeService;
        private readonly LeavePolicyService _leavePolicyService;

        public LeaveTypeController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _leaveTypeService = kernel.GetService(typeof(LeaveTypeService)) as LeaveTypeService;
                _leavePolicyService = kernel.GetService(typeof(LeavePolicyService)) as LeavePolicyService;
            }
        }

        public ActionResult LeaveTypeList()
        {
            return View();
        }

        public ActionResult Index(long? eid)
        {
            var leaveTypeView = new LeaveTypeViewModel
            {
                LeavePolicyList = new SelectList(_leavePolicyService.GetAll(), "Id", "PolicyName"),
            };
            return View(leaveTypeView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var leaveTypeList = _leaveTypeService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(leaveTypeList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save(LeaveTypeModel leaveType, bool isInsert)
        {
            if (isInsert)
            {
                leaveType.SetCreateProperties(LoginInformation.UserInformation.Id);
                leaveType.Id = _leaveTypeService.Insert(leaveType);
            }
            else
            {
                leaveType.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _leaveTypeService.Update(leaveType);
            }
            return new JsonResult { Data = leaveType };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _leaveTypeService.GetById(id) };
        }

        public void Delete(long id)
        {
            _leaveTypeService.DeleteSoftly(id);
        }
    }
}