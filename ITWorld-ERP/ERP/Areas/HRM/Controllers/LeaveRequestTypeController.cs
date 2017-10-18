using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class LeaveRequestTypeController : Controller
    {
        private readonly ILeaveRequestTypeService _leaveRequestTypeService;

        public ActionResult Index()
        {
            return View();
        }

        public LeaveRequestTypeController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _leaveRequestTypeService = kernel.GetService(typeof(LeaveRequestTypeService)) as LeaveRequestTypeService;
            }
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var leaveRequestTypeList = _leaveRequestTypeService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(leaveRequestTypeList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,RequestTypeName,Description")] LeaveRequestTypeModel leaveRequestType, bool isInsert)
        {
            if (isInsert)
            {
                leaveRequestType.SetCreateProperties(LoginInformation.UserInformation.Id);
                leaveRequestType.Id = _leaveRequestTypeService.Insert(leaveRequestType);
            }
            else
            {
                leaveRequestType.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _leaveRequestTypeService.Update(leaveRequestType);
            }
            return new JsonResult { Data = leaveRequestType };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _leaveRequestTypeService.GetById(id) };
        }

        public void Delete(long id)
        {
            _leaveRequestTypeService.DeleteSoftly(id);
        }
    }
}