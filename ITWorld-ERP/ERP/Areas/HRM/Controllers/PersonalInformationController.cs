using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.HRM.Domain;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class PersonalInformationController : Controller
    {
        private readonly IPersonalInformationService _personalInformationService;
        private readonly IAddressService _addressService;

        public PersonalInformationController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _personalInformationService = kernel.GetService(typeof(PersonalInformationService)) as PersonalInformationService;
                _addressService = kernel.GetService(typeof(AddressService)) as AddressService;
            }
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save(PersonalInformationModel personalInformation, bool isInsert)
        {
            if (isInsert)
            {
                personalInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                personalInformation.Id = _personalInformationService.Insert(personalInformation);
            }
            else
            {
                personalInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _personalInformationService.Update(personalInformation);
            }

            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName); // Save the file

                //r.Add(new ViewDataUploadFilesResult()
                //{
                //    Name = hpf.FileName,
                //    Length = hpf.ContentLength,
                //    Type = hpf.ContentType
                //});
            }

            personalInformation.Address.PersonId = personalInformation.Id;

            if (personalInformation.Address.Id == 0)
            {
                personalInformation.Address.SetCreateProperties(LoginInformation.UserInformation.Id);
                _addressService.Insert(personalInformation.Address);
            }
            else
            {
                personalInformation.Address.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _addressService.Update(personalInformation.Address);
            }

            return new JsonResult { Data = _personalInformationService.GetById(personalInformation.Id) };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _personalInformationService.GetById(id) };
        }

        [System.Web.Mvc.HttpPost]
        public ContentResult UploadFiles()
        {
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                var fileName = Path.GetFileName(hpf.FileName);
                if (fileName != null)
                {
                    string savedFileName = Path.Combine(Server.MapPath("~/Content/PhotosSignature"), fileName);
                    hpf.SaveAs(savedFileName);
                    return Content(fileName, "application/json");
                }
            }
            return null;
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/PhotosSignature"), fileName);
                file.SaveAs(path);
            }
            return View("Index");
        }

        //[System.Web.Mvc.HttpPost]
        //public ActionResult FileUpload(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        string pic = Path.GetFileName(file.FileName);
        //        string path = Path.Combine(Server.MapPath("~/Content/images"), pic);
        //        // file is uploaded
        //        file.SaveAs(path);

        //        // save the image path path to the database or you can send image
        //        // directly to database
        //        // in-case if you want to store byte[] ie. for DB
        //        using (var ms = new MemoryStream())
        //        {
        //            file.InputStream.CopyTo(ms);
        //            byte[] array = ms.GetBuffer();
        //        }

        //    }
        //    // after successfully uploading redirect the user
        //    return RedirectToAction("Index", "Employee");
        //}
    }
}