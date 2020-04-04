using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Service.Services;
using AppProj.Web.Models;
using AppProj.Web.ViewModels;
using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    [CustomAuthorize(Roles: new string[] { "USER" })]
    public class StandingController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IUnitOfWork unitOfWork;

        public StandingController(IUnitOfWork unitOfWork, IStandingDataService standingDataService)
        {
            this.standingDataService = standingDataService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index(int id)
        {
            ViewBag.Tp = id;

            SessionHelper.Temp = id;

            if (id == 1)
            {
                ViewBag.Title = "Program";
            }
            else if (id == 2)
            {
                ViewBag.Title = "Donor";
            }
            else if (id == 3)
            {
                ViewBag.Title = "Operation Mail Groups";
            }
            else if (id == 4)
            {
                ViewBag.Title = "Senior Mail Groups";
            }
            else if (id == 5)
            {
                ViewBag.Title = "PSU Mail Groups";
            }
            return View();
        }

        public ActionResult Create()
        {
            StandingDataModel up = new StandingDataModel();
            up.IsActive = true;

            int id = (int)SessionHelper.Temp;

            if (id == 1)
            {
                up.Type = "SRC";
            }
            

            return PartialView(up);
        }

        public ActionResult Edit(int Id)
        {
            var entity = standingDataService.GetDataById(Id);

            StandingDataModel model = new StandingDataModel();
            ModelCopier.CopyModel(entity, model);

            return PartialView("Create", model);
        }

        public ActionResult Save(StandingDataModel model)
        {
            StandingData entity = new StandingData();

            ModelCopier.CopyModel(model, entity);


            if (model.Id == 0)
            {
                standingDataService.Add(entity);
            }
            else
            {
                standingDataService.Update(entity);
            }

            unitOfWork.Commit();

            return PartialView();
        }

        public JsonResult DataGrid()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);

            IEnumerable<StandingData> projList = null;

            int id = (int)SessionHelper.Temp;
            
            if (id == 1)
            {
                projList = standingDataService.GetSource();
            }
            
            var obj = (from c in projList
                       select new object[] { c.Name,c.StringValue, c.IsActive?"Active":"Inactive"
                ,new GridButtonModel[]
                    {
                        new GridButtonModel{U=Url.Action("Edit",new {id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-info btn-mini\""},                 
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
