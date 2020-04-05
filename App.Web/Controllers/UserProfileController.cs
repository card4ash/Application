using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Globalization;
using Microsoft.Web.Mvc;
using AppProj.Service.Services;
using AppProj.Domain;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using AppProj.Web.Models;
using System.Text;
using AppProj.Web.Helpers;
using AppProj.Data.Infrastructure;
using AppProj.Web.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using AppProj.Service.StaffInfoServiceReference;
using Newtonsoft.Json;

namespace AppProj.Web.Controllers
{
    [Authorize]
    [CustomAuthorize(Roles: new string[] { "USER" })]
    public class UserProfileController : Controller
    {
        readonly IUserProfileService service;
        readonly IRoleService roleService;
        readonly IRoleFeatureService roleFeatureService;
        readonly IStandingDataService standingDataService;
        readonly IUnitOfWork unitOfWork;
        readonly IDistrictByUserProfileService districtByUserProfileService;
        readonly int userId = SessionHelper.UserId;

        public UserProfileController(IUserProfileService service, IRoleService roleService, IDistrictByUserProfileService districtByUserProfileService
            , IStandingDataService standingDataService, IRoleFeatureService roleFeatureService, IUnitOfWork unitOfWork)
        {
            this.service = service;
            this.roleService = roleService;
            this.standingDataService = standingDataService;
            this.roleFeatureService = roleFeatureService;
            this.districtByUserProfileService = districtByUserProfileService;
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            UserProfileModel up = new UserProfileModel();
            up.Roles = roleService.GetAll().ToSelectList(null, "Id", "RoleName");
            up.IsActive = true;

            return PartialView(up);
        }

        public ActionResult Edit(int Id)
        {
            var entity = service.GetDataById(Id);

            ViewBag.RoleId = new SelectList(roleService.GetAll(), "Id", "RoleName", entity.RoleId);

            UserProfileModel model = new UserProfileModel();
            ModelCopier.CopyModel(entity, model);

            return PartialView("Create", model);
        }

        public ActionResult Save(UserProfileModel model)
        {
            var login = service.GetByPin(model.Pin);

            if (login != null && model.Id <= 0)
            {
                model.Roles = roleService.GetAll().ToSelectList(null, "Id", "RoleName");

                ModelState.AddModelError("", "This user has already added");
                return PartialView("Create", model);
            }


            if (model.Id <= 0 && "" + model.Password != "")
            {
                model.Password = "" + model.Password.ToMD5();
            }
            
            

            if (model.Id > 0)
            {
                login.UserName = model.UserName;
                login.EmailAddress = model.EmailAddress;
                login.RoleId = model.RoleId;
                login.IsActive = model.IsActive;
                service.Update(login);
            }
            else
            {
                UserProfile entity = new UserProfile();
                ModelCopier.CopyModel(model, entity);
                service.Add(entity);
            }

            unitOfWork.Commit();
            SessionHelper.Temp = null;

            return PartialView();

        }

        public ActionResult Details(int id)
        {
            UserProfile entity = service.GetDataById(id);
            UserProfileModel model = new UserProfileModel();
            ModelCopier.CopyModel(entity, model);
            return PartialView(model);
        }

        public ActionResult Districts(int Id)
        {
            try
            {
                UserDistrictModel up = new UserDistrictModel();
                up.UserInfoId = Id;
                up.DistrictByUserProfile = districtByUserProfileService.GetByUser(Id).ToList();

                IEnumerable<int> ids = up.DistrictByUserProfile.Select(c => c.DistrictId);

                var allDis = standingDataService.GetDistricts().OrderBy(c => c.Name).ToList(); ;
                allDis.RemoveAll(c => ids.Contains(c.Id));

                up.RestDistricts = allDis;

                return PartialView("Districts", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        public ActionResult DistrictsAdd(int Id, int UserInfoId)
        {
            try
            {
                DistrictByUserProfile entity = new DistrictByUserProfile();
                entity.UserInfoId = UserInfoId;
                entity.DistrictId = Id;

                districtByUserProfileService.Add(entity);
                unitOfWork.Commit();

                UserDistrictModel up = new UserDistrictModel();

                up.UserInfoId = UserInfoId;

                up.DistrictByUserProfile = districtByUserProfileService.GetByUser(UserInfoId).ToList();

                IEnumerable<int> ids = up.DistrictByUserProfile.Select(c => c.DistrictId);

                var allDis = standingDataService.GetDistricts().OrderBy(c => c.Name).ToList(); ;
                allDis.RemoveAll(c => ids.Contains(c.Id));

                up.RestDistricts = allDis;

                return PartialView("Districts", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        public ActionResult DistrictsDelete(int Id)
        {

            try
            {
                var entity = districtByUserProfileService.Get(Id);
                districtByUserProfileService.Delete(entity);
                unitOfWork.Commit();


                UserDistrictModel up = new UserDistrictModel();
                up.UserInfoId = entity.UserInfoId;
                up.DistrictByUserProfile = districtByUserProfileService.GetByUser(up.UserInfoId).ToList();

                IEnumerable<int> ids = up.DistrictByUserProfile.Select(c => c.DistrictId);

                var allDis = standingDataService.GetDistricts().OrderBy(c => c.Name).ToList(); ;
                allDis.RemoveAll(c => ids.Contains(c.Id));

                up.RestDistricts = allDis;

                return PartialView("Districts", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        //public FileContentResult GetImage(int id)
        //{
        //    var data = service.GetDataById(id);
        //    return new FileContentResult(data.Photo ?? new byte[1], "image/png");
        //}

        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangePassword(UserProfileModel model)
        {
            if (model.NewPassword != null && model.NewPassword != null && model.NewPassword == model.ConfirmNewPassword)
            {
                int id = SessionHelper.UserId;
                var data = service.GetDataById(id);
                if (data.Password == model.Password.ToMD5())
                {
                    model.Id = id;
                    data.Password = model.NewPassword.ToMD5();
                    service.Update(data);
                    unitOfWork.Commit();
                    return PartialView("PasswordChanged");
                }

                ModelState.AddModelError("", "Wrong password was given");
            }

            return PartialView();
        }


        public ActionResult ResetPass(int id)
        {
            UserProfileModel model = new UserProfileModel();
            model.Id = id;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ResetPass(UserProfileModel model)
        {
            if (model.NewPassword != null && model.NewPassword != null && model.NewPassword == model.ConfirmNewPassword)
            {
                int id = model.Id;
                var data = service.GetDataById(id);

                model.Id = id;
                data.Password = model.NewPassword.ToMD5();
                service.Update(data);
                unitOfWork.Commit();
                return PartialView("PasswordChanged");
            }

            return PartialView();
        }

        public ActionResult PasswordChanged()
        {
            return PartialView();
        }

        public JsonResult GetProfileByPinTest()
        {
            List<StaffProfile> model = new List<StaffProfile>();
            model.Add(new StaffProfile { EmailID = "1566", StaffName = "adasd" });
            model.Add(new StaffProfile { EmailID = "15661", StaffName = "adas1d" });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProfileByPin(string pinId)
        {
            /*
            StaffInfoSoapClient st = new StaffInfoSoapClient();

            string jsonString = st.StaffInfoByPIN(pinId);

            List<StaffProfile> profile = JsonConvert.DeserializeObject<List<StaffProfile>>(jsonString);

                if (profile.Count() > 0)
                {
                    return Json(profile.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            */


            string URL = "http://api.brac.net/v1/staffs";
            string urlParameters = "?Key=d65808a7-699f-4d5c-88ee-01951e675cf2&fields=StaffName,EmailID&q=pin=" + pinId;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.

                var dataString = response.Content.ReadAsAsync<string>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                IEnumerable<StaffProfile> dataObjects = String.IsNullOrEmpty(dataString) ? null : JsonConvert.DeserializeObject<IEnumerable<StaffProfile>>(dataString);

                if (dataObjects != null)
                {
                    client.Dispose();
                    return Json(dataObjects.FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }

            client.Dispose();
            return Json(null, JsonRequestBehavior.AllowGet);


        }

        public JsonResult DataGrid()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);
            string searchText = Request.QueryString["searchText"];

            var data = service.GetAll(searchText, userId);

            var obj = (from c in data
                       select new object[] { c.Pin,c.UserName,c.EmailAddress,c.Role.RoleName,c.IsActive?"Active":"Inactive"
                ,new GridButtonModel[]
                    {
                        new GridButtonModel{U=Url.Action("Edit",new {id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit User"}
                        ,new GridButtonModel{U=Url.Action("Details",new {id=c.Id}), T="Details", D = GridButtonDialog.dialig1.ToString(),H="User Profile"}
                        ,new GridButtonModel{U=Url.Action("ResetPass",new {id=c.Id}), T="Password Reset", D = GridButtonDialog.dialig1.ToString(), H="Reset Password", V=(""+c.Password!="")}
                        ,new GridButtonModel{U=Url.Action("Districts",new {id=c.Id}), T="Districts", D = GridButtonDialog.dialig1.ToString(),H="Assigned Districts"}

                    }
            }).Skip(tke).Take(skp).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = data.Count().ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}


