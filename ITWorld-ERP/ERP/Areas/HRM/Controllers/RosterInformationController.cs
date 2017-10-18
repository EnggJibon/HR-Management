using System.Web.Mvc;
using System.Web.Http;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class RosterInformationController : Controller
    {
        private readonly IRosterInformationService _roosterInformationService;

        public RosterInformationController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (karnel != null)
            {
                _roosterInformationService = karnel.GetService(typeof(RosterInformationService)) as RosterInformationService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var rosterInformationList = _roosterInformationService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(rosterInformationList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Name,StartTime,EndTime,Description,IsActive")] RosterInformationModel rosterInformation, bool isInsert)
        {
            if (isInsert)
            {
                rosterInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                rosterInformation.Id = _roosterInformationService.Insert(rosterInformation);
            }
            else
            {
                rosterInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _roosterInformationService.Update(rosterInformation);
            }
            return new JsonResult { Data = rosterInformation };
        }

        public JsonResult Get(long id)
        {
            var rosterInfo = _roosterInformationService.GetById(id);
            return new JsonResult { Data =  rosterInfo};
        }

        public void Delete(long id)
        {
            _roosterInformationService.DeleteSoftly(id);
        }
    }
}