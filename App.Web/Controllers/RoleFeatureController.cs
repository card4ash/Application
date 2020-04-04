using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppProj.Domain;
using AppProj.Web.Models;
using AppProj.Service.Services;
using AppProj.Web.Helpers;
using AppProj.Web.ViewModels;
using AppProj.Data.Infrastructure;


namespace AppProj.Web.Controllers
{
    [Authorize]
    [CustomAuthorize(Roles: new string[] { "USER" })]
    public class RoleFeatureController : Controller
    {
        readonly IRoleFeatureService roleFeatureService;
        readonly IRoleService roleService;
        readonly IFeatureService featureService;
        readonly IUnitOfWork unitOfWork;

        public RoleFeatureController(IRoleFeatureService roleFeatureService
            , IFeatureService featureService,IRoleService roleService, IUnitOfWork unitOfWork)
        {
            this.roleFeatureService = roleFeatureService;
            this.roleService = roleService;
            this.featureService = featureService;
            this.unitOfWork = unitOfWork;       
        }

        public ActionResult Index(int roleId)
        {
            ViewBag.RoleId = roleId;
            ViewBag.RoleName = roleService.GetDataById(roleId).RoleName;
            return View(roleFeatureService.GetFeaturesByRoleID(roleId).OrderBy(r => r.Name));
        }        

        public ActionResult AddFeature(int roleId)
        {
            var fr = (from c in featureService.GetFeaturesList() where !(from d in roleFeatureService.GetFeaturesByRoleID(roleId) select d.FeatureId).Contains(c.ID) select c);

            RoleFeatureModel model = new RoleFeatureModel();
            model.RoleId = roleId;
            ViewBag.FeatureId = new SelectList(fr, "ID", "Description");

            return PartialView(model);
        }

        public ActionResult Save(RoleFeatureModel model)
        {

            RoleFeature rf = new RoleFeature();
            rf.FeatureId = model.FeatureId;
            rf.RoleId = model.RoleId;
           
            roleFeatureService.Add(rf);
            unitOfWork.Commit();

            return PartialView();
        }

        public ActionResult Delete(int id)
        {
            roleFeatureService.Delete(id);
            unitOfWork.Commit();

            return PartialView();
        }

        public JsonResult DataGrid()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);
            int roleId = int.Parse(Request.QueryString["roleId"]);

            var projList = roleFeatureService.GetFeaturesByRoleID(roleId).OrderBy(r => r.Name);

            var obj = (from c in projList
                       select new object[] { c.Description
                ,new GridButtonModel[]
                    {
                        new GridButtonModel{U=Url.Action("Delete",new {id=c.Id}), T="Delete", D = GridButtonDialog.dialig1.ToString(), H="Delete Feature",M="class=\"btn btn-info btn-mini\""}                        
                    }
            }).Skip(tke).Take(skp).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = projList.Count().ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}
