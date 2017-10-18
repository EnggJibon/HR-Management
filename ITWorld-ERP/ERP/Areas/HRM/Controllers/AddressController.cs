using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _addressService = kernel.GetService(typeof(AddressService)) as AddressService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var addressList = _addressService.GetAll();
            return Json(addressList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Address1,Address2,CountryId,City,State,PostalCode,IsActive")] AddressModel address, bool isInsert)
        {
            if (isInsert)
            {
                address.SetCreateProperties(LoginInformation.UserInformation.Id);
                address.Id = _addressService.Insert(address);
            }
            else
            {
                address.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _addressService.Update(address);
            }
            return new JsonResult { Data = _addressService.GetById(address.Id) };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _addressService.GetById(id) };
        }

        public void Delete(long id)
        {
            _addressService.DeleteSoftly(id);
        }
    }
}

