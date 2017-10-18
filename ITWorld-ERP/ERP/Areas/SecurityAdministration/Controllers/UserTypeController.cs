using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Security;
using ERP.Security.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class UserTypeController : Controller
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _userTypeService = kernel.GetService(typeof(UserTypeService)) as UserTypeService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var userTypes = _userTypeService.GetAll();
            return Json(userTypes.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, Name, Description, IsActive")] UserTypeModel userType, bool isInsert)
        {
            if (isInsert)
            {
                userType.SetCreateProperties(LoginInformation.UserInformation.Id);
                userType.Id = _userTypeService.Insert(userType);
            }
            else
            {
                userType.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _userTypeService.Update(userType);
            }
            return new JsonResult { Data = userType};
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _userTypeService.GetById(id) };
        }

        public void Delete(long id)
        {
            _userTypeService.DeleteSoftly(id);
        }
    }
}