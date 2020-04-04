using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppProj.Domain;
using AppProj.Service.Services;
using Microsoft.Web.Mvc;
using AppProj.Web.Helpers;
using AppProj.Web.Models;
using AppProj.Web.ViewModels;
using AppProj.Data.Infrastructure;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AppProj.Web.Controllers
{
    [Authorize]
    [CustomAuthorize(Roles: new string[] { "USER" })]
    public class RoleController : Controller
    {
        readonly IRoleService roleService;
        readonly IRoleDefaultPageService defaultService;
        readonly IUnitOfWork unitOfWork;

        public RoleController(IRoleService roleService, IRoleDefaultPageService defaultService, IUnitOfWork unitOfWork)
        {
            this.defaultService = defaultService;
            this.roleService = roleService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {            
            return View();
        }
                
        public ActionResult Create()
        {
            RoleModel up = new RoleModel();
            up.RoleDefaultPages = defaultService.GetRoleDefaultPageList().ToSelectList(null, "Id", "Name");
            up.IsActive = true;
            return PartialView(up);
        }

        public ActionResult Edit(int Id)
        {
            try
            {
                var entity = roleService.GetDataById(Id);

                RoleModel model = new RoleModel();
                model.RoleDefaultPages = defaultService.GetRoleDefaultPageList().ToSelectList(null, "Id", "Name");
                ModelCopier.CopyModel(entity, model);

                return PartialView("Create", model);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        public ActionResult Save(RoleModel model)
        {
            try
            {                
                Role entity = new Role();
               
                ModelCopier.CopyModel(model, entity);


                if (model.Id == 0)
                {
                    roleService.Add(entity);                    
                }
                else
                {
                    roleService.Update(entity);                    
                }
                
                unitOfWork.Commit();

                return PartialView();
            }
            catch
            {                
                return RedirectToAction("Error", "Main");
            }
        }

        public JsonResult DataGrid()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);

            var projList = roleService.GetAll();

            var obj = (from c in projList
                       select new object[] { c.RoleName, c.RoleDefaultPage.Name, c.IsActive?"Active":"Inactive"
                ,new GridButtonModel[]
                    {
                        new GridButtonModel{U=Url.Action("Edit",new {id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit Role", M="class=\"btn btn-info btn-mini\""}
                        ,new GridButtonModel{U=Url.Action("Index","RoleFeature",new {roleId=c.Id}), T="Rights", M="class=\"btn btn-info btn-mini\"", A=false}
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
