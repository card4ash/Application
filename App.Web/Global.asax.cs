using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Data.RepositoriesImpl;
using AppProj.Service.Services;
using AppProj.Service.ServicesImpl;
using AppProj.Web.IOC;
using Microsoft.Practices.Unity;

namespace AppProj.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            IUnityContainer container = GetUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        private IUnityContainer GetUnityContainer()
        {
            //Create UnityContainer          
            IUnityContainer container = new UnityContainer()

                //
                //.RegisterType<I##Repository, ##Repository>(new HttpContextLifetimeManager<I##Repository>())
                //.RegisterType<I##Service, ##Service>(new HttpContextLifetimeManager<I##Service>())

                /*------------<<<<<<<<<<<<<   Global >>>>>>>>>>>-------------------*/
                .RegisterType<IControllerActivator, CustomControllerActivator>()
                .RegisterInstance<MembershipProvider>(Membership.Provider)
                .RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>())
                .RegisterType<IUnitOfWork, UnitOfWork>(new HttpContextLifetimeManager<IUnitOfWork>())

                /*------------<<<<<<<<<<<<<   Repositories >>>>>>>>>>>-------------------*/
                .RegisterType<IDistrictByUserProfileRepository, DistrictByUserProfileRepository>(new HttpContextLifetimeManager<IDistrictByUserProfileRepository>())
                .RegisterType<IFeatureRepository, FeatureRepository>(new HttpContextLifetimeManager<IFeatureRepository>())
                .RegisterType<IQryRoleFeatureRepository, QryRoleFeatureRepository>(new HttpContextLifetimeManager<IQryRoleFeatureRepository>())                
                .RegisterType<IRoleDefaultPageRepository, RoleDefaultPageRepository>(new HttpContextLifetimeManager<IRoleDefaultPageRepository>())
                .RegisterType<IRoleFeatureRepository, RoleFeatureRepository>(new HttpContextLifetimeManager<IRoleFeatureRepository>())
                .RegisterType<IRoleRepository, RoleRepository>(new HttpContextLifetimeManager<IRoleRepository>())
                .RegisterType<IStandingDataRepository, StandingDataRepository>(new HttpContextLifetimeManager<IStandingDataRepository>())
                .RegisterType<IUserProfileRepository, UserProfileRepository>(new HttpContextLifetimeManager<IUserProfileRepository>())
                .RegisterType<IUserLoginLogRepository, UserLoginLogRepository>(new HttpContextLifetimeManager<IUserLoginLogRepository>())
                .RegisterType<IDetailDataRepository, DetailDataRepository>(new HttpContextLifetimeManager<IDetailDataRepository>())
                .RegisterType<IDistrictDataRepository, DistrictDataRepository>(new HttpContextLifetimeManager<IDistrictDataRepository>())
                .RegisterType<IDistrictSummeryRepository, DistrictSummeryRepository>(new HttpContextLifetimeManager<IDistrictSummeryRepository>())
                .RegisterType<ISummerizedDataRepository, SummerizedDataRepository>(new HttpContextLifetimeManager<ISummerizedDataRepository>())
                .RegisterType<IHnppDataRepository, HnppDataRepository>(new HttpContextLifetimeManager<IHnppDataRepository>())

                .RegisterType<IDistrictByUserProfileService, DistrictByUserProfileService>(new HttpContextLifetimeManager<IDistrictByUserProfileService>())
                .RegisterType<IFeatureService, FeatureService>(new HttpContextLifetimeManager<IFeatureService>())
                .RegisterType<IRoleDefaultPageService, RoleDefaultPageService>(new HttpContextLifetimeManager<IRoleDefaultPageService>())
                .RegisterType<IRoleFeatureService, RoleFeatureService>(new HttpContextLifetimeManager<IRoleFeatureService>())
                .RegisterType<IRoleService, RoleService>(new HttpContextLifetimeManager<IRoleService>())
                .RegisterType<IStandingDataService, StandingDataService>(new HttpContextLifetimeManager<IStandingDataService>())
                .RegisterType<IUserProfileService, UserProfileService>(new HttpContextLifetimeManager<IUserProfileService>())
                .RegisterType<IUserLoginLogService, UserLoginLogService>(new HttpContextLifetimeManager<IUserLoginLogService>())
                .RegisterType<IDetailDataService, DetailDataService>(new HttpContextLifetimeManager<IDetailDataService>())
                .RegisterType<IDistrictDataService, DistrictDataService>(new HttpContextLifetimeManager<IDistrictDataService>())
                .RegisterType<ISummerizedDataService, SummerizedDataService>(new HttpContextLifetimeManager<ISummerizedDataService>())
                .RegisterType<IHnppDataService, HnppDataService>(new HttpContextLifetimeManager<IHnppDataService>())

            ;

            return container;
        }
    }

    #region Authentication & Authorization

    public class CustomAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public string Feature { get; set; }
        public AuthType AuthType { get; set; }
        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] Roles)
        {
            Feature = "";
            this.allowedroles = Roles;
            AuthType = AuthType.View;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            List<string> roles = null;
            try
            {
                roles = (List<string>)SessionHelper.Role;
            }
            catch { }
            finally
            {
                roles = roles ?? new List<string>();
            }

            var CommonList = roles.Intersect(allowedroles);

            if (CommonList.Count()>0) return true;


            return false;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isPermit = false;

            if (filterContext.HttpContext.User.Identity.Name == "")
            {
                filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                                        {
                                                { "client", filterContext.RouteData.Values[ "client" ] },
                                                { "controller", "Home" },
                                                { "action", "Index" },
                                                {"area",""},
                                                { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                                        });
            }
            else
            {
                isPermit = true;
                base.OnAuthorization(filterContext);
            }

            if (isPermit && Feature != "")
            {
                //isPermit = 

                /*switch (AuthType)
                {
                    case AuthType.Delete:
                        isPermit = UserRole.CheckDelete(Feature, SessionHelper.Role);
                        break;

                    case AuthType.Edit:
                        isPermit = UserRole.CheckEdit(Feature, SessionHelper.Role);
                        break;

                    case AuthType.Preview:
                        isPermit = UserRole.CheckPreview(Feature, SessionHelper.Role);
                        break;

                    case AuthType.Print:
                        isPermit = UserRole.CheckPrint(Feature, SessionHelper.Role);
                        break;

                    case AuthType.Save:
                        isPermit = UserRole.CheckSave(Feature, SessionHelper.Role);
                        break;

                    case AuthType.View:
                        isPermit = UserRole.CheckSave(Feature, SessionHelper.Role);
                        break;

                    default:
                        isPermit = false;
                        break;
                }*/

                if (!isPermit)
                {
                    filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary
                                   {
                                           { "client", filterContext.RouteData.Values[ "client" ] },
                                           { "controller", "Main" },
                                           { "action", "UnAuthorisedAction" },
                                           {"area",""}                                           
                                   });
                }
            }
        }
    }

    public enum AuthType
    {
        View
        ,
        Save
          ,
        Delete
          ,
        Edit
          ,
        Preview
            ,
        Print
    }

    #endregion

    #region Session

    public static class SessionHelper
    {

        //public static string DateFormat
        //{
        //    get { return GetFromSession<string>("dateformat"); }
        //    set { SetInSession("dateformat", value); }
        //}

        public static int UnitId
        {
            get { return GetFromSession<int>("unit_id"); }
            set { SetInSession("unit_id", value); }
        }    

        public static int UserId
        {
            get { return GetFromSession<int>("id"); }
            set { SetInSession("id", value); }
        }

        public static bool IsGuest
        {
            get { return GetFromSession<bool>("isguest"); }
            set { SetInSession("isguest", value); }
        }

        public static string UserName
        {
            get { return GetFromSession<string>("name"); }
            set { SetInSession("name", value); }
        }
        
        public static List<string> Role
        {
            get { return GetFromSession<List<string>>("role"); }
            set { SetInSession("role", value); }
        }

        public static string DefaultPage
        {
            get { return GetFromSession<string>("default_page"); }
            set { SetInSession("default_page", value); }
        }

        public static object Temp
        {
            get { return GetFromSession("temp"); }
            set { SetInSession("temp", value); }
        }

        public static object Temp2
        {
            get { return GetFromSession("temp2"); }
            set { SetInSession("temp2", value); }
        }

        public static object Temp3
        {
            get { return GetFromSession("temp3"); }
            set { SetInSession("temp3", value); }
        }

        public static object Temp4
        {
            get { return GetFromSession("temp4"); }
            set { SetInSession("temp4", value); }
        }

        private static object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        private static T GetFromSession<T>(string key)
        {
             return (T)HttpContext.Current.Session[key];
        }
        private static void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }

    #endregion

    }