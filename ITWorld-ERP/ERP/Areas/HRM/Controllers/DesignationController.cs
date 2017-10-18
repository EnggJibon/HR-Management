using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;

        public ActionResult Index()
        {
            return View();
        }

        public DesignationController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _designationService = kernel.GetService(typeof(DesignationService)) as DesignationService;
            }
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var designationList = _designationService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(designationList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Name,Description,IsActive")] DesignationModel designation, bool isInsert)
        {
            if (isInsert)
            {
                designation.SetCreateProperties(LoginInformation.UserInformation.Id);
                designation.Id = _designationService.Insert(designation);
            }
            else
            {
                designation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _designationService.Update(designation);
            }
            return new JsonResult { Data = designation };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _designationService.GetById(id) };
        }

        public void Delete(long id)
        {
            _designationService.DeleteSoftly(id);
        }




    }
}