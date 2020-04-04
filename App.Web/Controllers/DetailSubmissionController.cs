using AppProj.Data.Infrastructure;
using AppProj.Service.Services;
using AppProj.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppProj.Web.Helpers;
using AppProj.Domain;
using AppProj.Web.ViewModels;
using Microsoft.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class DetailSubmissionController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IDetailDataService sunDataService;
        readonly IUnitOfWork unitOfWork;

        public DetailSubmissionController(IUnitOfWork unitOfWork, IDetailDataService sunDataService, IStandingDataService standingDataService)
        {
            this.standingDataService = standingDataService;
            this.sunDataService = sunDataService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts().Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            var upa = standingDataService.GetUpazilla(0).Where(r => r.IsActive);
            up.ContentTypes3 = upa.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        [CustomAuthorize(Roles: new string[] { "DETAIL" })]
        public ActionResult Create()
        {
            return PartialView(GetModel());            
        }

        [CustomAuthorize(Roles: new string[] { "DETAIL" })]
        public ActionResult Edit(int Id)
        {
            try
            {
                var entity = sunDataService.Get(Id);

                SubmitModel up = new SubmitModel();

                up.Sources = standingDataService.GetSource().Where(r => r.IsActive).ToSelectList(up.SourceId, "Id", "Name");
                up.Districts = standingDataService.GetDistricts().Where(r => r.IsActive).ToSelectList(up.DistrictId, "Id", "Name");
                up.Upazillas = standingDataService.GetUpazilla(entity.DistrictId).Where(r => r.IsActive).ToSelectList(up.UpazillaId, "Id", "Name");
                up.Genders = standingDataService.GetGender().Where(r => r.IsActive).ToSelectList(up.GenderId, "Id", "Name");

                ModelCopier.CopyModel(entity, up);

                return PartialView("Create", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        [CustomAuthorize(Roles: new string[] { "DETAIL" })]
        public ActionResult Save(SubmitModel model)
        {
            SessionHelper.Temp = model.DistrictId;
            SessionHelper.Temp2 = model.UpazillaId;
            SessionHelper.Temp3 = model.CollectedBy;
            SessionHelper.Temp4 = model.SourceId;

            
            DetailData entity = new DetailData();
            
            ModelCopier.CopyModel(model, entity);

            entity.InsertedById = SessionHelper.UserId;

            if (model.Id == 0)
            {
                sunDataService.Add(entity);
            }
            else
            {
                sunDataService.Update(entity);
            }

            unitOfWork.Commit();

            return PartialView();

            //return PartialView("Create",GetModel(model.CollectedBy));

        }

        public JsonResult DataGrid()
        {

            bool visible = UserRole.Check("DETAIL", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            if (take == -1) { take = 100000000; skip = 0; }

            int? srcId = null;

            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? disId = null;

            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }

            int? upzId = null;
            try
            {
                upzId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }

            bool onlyPatient= Convert.ToBoolean(Request.QueryString["onlyPatient"]);

            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            IEnumerable<DetailData> dataList = sunDataService.Get(srcId, disId, upzId, FromDate, ToDate, onlyPatient,skip,take,out count);


            var obj = (from c in dataList
                       select new object[] {c.StandingData.Name
                       ,c.StandingData1.Name
                       ,c.StandingData2.Name
                       , String.Format("{0:dd MMM, yyyy}", c.Date)
                       ,c.Name
                       ,c.Age
                       ,c.IsFever==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       ,c.IsDryCough==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       ,c.IsBreadth==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       ,c.IsContact==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       , c.StandingData3.Name
                       ,c.Phone
                       ,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\"", V = visible}

                    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexApp()
        {
            SearchModel up = new SearchModel();

            //var source = standingDataService.GetSource().Where(r => r.IsActive);
            //up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            //var dis = standingDataService.GetDistricts().Where(r => r.IsActive);
            //up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            //var upa = standingDataService.GetUpazilla(0).Where(r => r.IsActive);
            //up.ContentTypes3 = upa.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }
        public JsonResult DataGridApp()
        {

            bool visible = UserRole.Check("DETAIL", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            if (take == -1) { take = 100000000; skip = 0; }

            int? srcId = null;

            try
            {
                //srcId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? disId = null;

            try
            {
                //disId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }

            int? upzId = null;
            try
            {
                //upzId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }

            
            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            IEnumerable<DetailData> dataList = sunDataService.GetAppData(srcId, disId, upzId, FromDate, ToDate, skip, take, out count);


            var obj = (from c in dataList
                       select new object[] {
                       //c.StandingData.Name
                       //,c.StandingData1.Name
                       //,c.StandingData2.Name
                       String.Format("{0:dd MMM, yyyy}", c.Date)
                       //,c.Name
                       //,c.Age
                       ,c.IsFever==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       ,c.IsDryCough==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       ,c.IsBreadth==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       ,c.IsContact==true?"<span style=\"color:#FF0000\">হ্যা</span>":"না"
                       //,c.StandingData3.Name
                       ,c.CollectedBy
                       ,c.Phone
                       ,c.Description
                       //,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\"", V = visible}

                    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        SubmitModel GetModel()
        {
            int d = 0;
            int.TryParse("" + SessionHelper.Temp, out d);

            int u = 0;
            int.TryParse("" + SessionHelper.Temp2, out u);

            int s = 0;
            int.TryParse("" + SessionHelper.Temp4, out s);

            SubmitModel up = new SubmitModel();

            up.Sources = standingDataService.GetSource().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");
            up.Districts = standingDataService.GetDistricts().Where(r => r.IsActive).ToSelectList(d, "Id", "Name");

            int i = (d == 0 ? int.Parse(up.Districts.FirstOrDefault().Value) : d);
            up.Upazillas = standingDataService.GetUpazilla(i).Where(r => r.IsActive).ToSelectList(u, "Id", "Name");

            up.Genders = standingDataService.GetGender().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.DistrictId = d;
            up.UpazillaId = u;
            up.SourceId = s;
            up.Date = DateTime.Now;
            up.CollectedBy = ""+SessionHelper.Temp3;
            

            up.Name = "";
            up.Phone = "";
            up.Age = null;
            up.Description = "";
            up.IsBreadth = false;
            up.IsContact = false;
            up.IsDryCough = false;
            up.IsFever = false;


            return up;
        }


    }
}
