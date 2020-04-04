using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Service.Services;
using AppProj.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppProj.Web.Helpers;

namespace AppProj.Web.Controllers
{
    public class LoginController : Controller
    {
        IUserProfileService userProfileService;
        IUserLoginLogService userLoginLogService;
        IRoleService roleService;
        IRoleFeatureService roleFeatureService;
        readonly IUnitOfWork unitOfWork;

        public LoginController(IUserProfileService userProfileService
            , IUserLoginLogService userLoginLogService
            , IRoleService roleService
            , IRoleFeatureService roleFeatureService
            , IUnitOfWork unitOfWork)
        {
            this.userProfileService = userProfileService;
            this.userLoginLogService = userLoginLogService;
            this.roleService = roleService;
            this.roleFeatureService = roleFeatureService;
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogOnModel model)
        {
            if (IsAuthorised(model.user, ""+model.password))
            {
                return Redirect("~/" + SessionHelper.DefaultPage);
            }

            ModelState.AddModelError("", "User Id or password was given wrong");

            return View(model);
        }

        bool IsAuthorised(string pin, string password)
        {
            var login = userProfileService.GetByPin(pin);

            if (login == null)
            {
                return false;
            }

            else if (login.Password != password.ToMD5())
            {
                return false;
            }

            Role role = roleService.GetDataById(login.RoleId);

            if (role.IsActive && login.IsActive)
            {
                userLoginLogService.Add(new UserLoginLog
                {
                    PIN = login.Pin
                    ,
                    Name = login.UserName
                    ,
                    LoginTime = DateTime.Now
                });

                unitOfWork.Commit();

                FormsAuthentication.SetAuthCookie(pin, true);
                var identity = new GenericIdentity(pin);

                //var unit = unitOfUserService.GetAllUnitsOfUserByUserProfileId(login.Id).FirstOrDefault();
                //SessionHelper.DateFormat = "dd MMM, yyyy";
                SessionHelper.UserName = login.UserName;
                SessionHelper.UserId = login.Id;
                SessionHelper.IsGuest = true;

                //var tmp = userAccessListService.GetUserAccessListByUserProfileID(login.Id);
                //SessionHelper.Role = roleFeatureService.GetFeaturesByRoleID(login.RoleId);
                //SessionHelper.CanAccessAllDept = role.CanAccessAllDepartment;
                IEnumerable<QryRoleFeature> features = roleFeatureService.GetFeaturesByRoleID(role.Id);
                List<string> ftr = new List<string>();

                if (features != null)
                {
                    ftr = features.Select(c => c.Name).ToList();
                }

                SessionHelper.Role = ftr;

                GenericPrincipal gp = new GenericPrincipal(identity, ftr.ToArray());
                HttpContext.User = gp;

                SessionHelper.DefaultPage = role.RoleDefaultPage.PageUrl;

                return true;
            }

            return false;
        }

    }
}
