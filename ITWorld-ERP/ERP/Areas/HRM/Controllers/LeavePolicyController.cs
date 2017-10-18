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
    public class LeavePolicyController : Controller
    {
        private readonly ILeavePolicyService _leavePolicyService;

        public LeavePolicyController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _leavePolicyService = kernel.GetService(typeof(LeavePolicyService)) as LeavePolicyService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var leavePolicyList = _leavePolicyService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(leavePolicyList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,PolicyName,Description,EffectiveDate,IsActive")] LeavePolicyModel leavePolicy, bool isInsert)
        {
            if (isInsert)
            {
                leavePolicy.SetCreateProperties(LoginInformation.UserInformation.Id);
                leavePolicy.Id = _leavePolicyService.Insert(leavePolicy);
            }
            else
            {
                leavePolicy.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _leavePolicyService.Update(leavePolicy);
            }
            return new JsonResult { Data = leavePolicy };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _leavePolicyService.GetById(id) };
        }

        public void Delete(long id)
        {
            _leavePolicyService.DeleteSoftly(id);
        }
    }
}