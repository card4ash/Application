using AppProj.Data;
using AppProj.Domain;
using AppProj.Service.Services;
using AppProj.Web.Filters;
using AppProj.Web.Helpers;
using AppProj.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using SSOClient.ModelManager;
using System.Security.Principal;
using AppProj.Data.Infrastructure;

namespace AppProj.Web.Controllers
{

    public class HomeController : Controller
    {        
        IUserProfileService userProfileService;
        IUserLoginLogService userLoginLogService;
        IRoleService roleService;
        IRoleFeatureService roleFeatureService;
        readonly IUnitOfWork unitOfWork;

        public HomeController(IUserProfileService userProfileService
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

        public ActionResult Index(string returnUrl)
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            //temp

            //if (IsAuthorised("154211"))
            //{
            //    //SessionHelper.IsGuest = true;
            //    return Redirect("~/" + SessionHelper.DefaultPage);
            //}
            //return Redirect("~/Main");
            //860538 source, 860539 dis, 860540 upz


            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                                
                if (IsAuthorised(ticket.Name))
                {
                    return Redirect("~/" + SessionHelper.DefaultPage);
                }
                else
                {
                    ModelState.AddModelError("", "");
                    return View("Index");
                }
            }

            return Redirect(SsoUtility.SsoSession + "?site=" + SsoUtility.Site);  
            //return View("Index");
        }

        
        public ActionResult Login()
        {
            
            //if (IsAuthorised("154211"))
            //{
            //    return Redirect("~/" + SessionHelper.DefaultPage);
            //}

            //SessionHelper.UserName = "Admin";
            //SessionHelper.UserId = 3;
            //SessionHelper.UnitId = 1;
            //SessionHelper.DateFormat = "dd MMM, yyyy";
            //return Redirect("~/Main");
            

            var encriptData = string.Empty;
            if (Request.QueryString[SsoUtility.SsoToken] != null)
            {
                encriptData = HttpUtility.HtmlDecode(Request.QueryString[SsoUtility.SsoToken]);
                
                
                //Process for normal authentication
                var objSsoManager = new SSOManager();
                var objSso = objSsoManager.GetSSO(encriptData);
                
                //SMSC.Models.SMSCContext _contex = new Models.SMSCContext();
                if (objSso.authenticated == true)
                {
                    userLoginLogService.Add(new UserLoginLog
                    { PIN = objSso.name
                    , Name = objSso.fullname
                    , LoginTime = DateTime.Now
                    });

                    unitOfWork.Commit();
                    SessionHelper.IsGuest = false;

                    if (IsAuthorised(objSso.name))
                    {
                        
                        return Redirect("~/" + SessionHelper.DefaultPage);
                    }
                    else
                    {
                        SessionHelper.UserName = objSso.fullname;
                        SessionHelper.UserId = 0;
                        SessionHelper.UnitId = 1;
                        //SessionHelper.DateFormat = "dd MMM, yyyy";
                        return Redirect("~/Main");                        
                    }
                }
                else if (objSso.name!=null)
                {
                    ModelState.AddModelError("", "");
                }

            }

            return View("Index");
        }
        

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public JsonResult HeartBeat()
        {
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Error()
        {
            return View();
        }

        bool IsAuthorised(string pin)
        {
            var login = userProfileService.GetByPin(pin);

            FormsAuthentication.SetAuthCookie(pin, true);
            var identity = new GenericIdentity(pin);

            if (login == null)
            {
                return false;
            }

            Role role = roleService.GetDataById(login.RoleId);

            if (role.IsActive && login.IsActive)
            {
                //var unit = unitOfUserService.GetAllUnitsOfUserByUserProfileId(login.Id).FirstOrDefault();
                //SessionHelper.DateFormat = "dd MMM, yyyy";
                SessionHelper.UserName = login.UserName;
                SessionHelper.UserId = login.Id;

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
