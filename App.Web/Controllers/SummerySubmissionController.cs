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
    public class SummerySubmissionController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly ISummerizedDataService sunDataService;
        readonly IUnitOfWork unitOfWork;

        public SummerySubmissionController(IUnitOfWork unitOfWork, ISummerizedDataService sunDataService, IStandingDataService standingDataService)
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

        [CustomAuthorize(Roles: new string[] { "SUMMERY" })]
        public ActionResult Create()
        {
            SubmitModel up = new SubmitModel();

            up.Sources = standingDataService.GetSource().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");
            up.Districts = standingDataService.GetDistricts().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            int i = int.Parse(up.Districts.FirstOrDefault().Value);
            up.Upazillas = standingDataService.GetUpazilla(i).Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.Date = DateTime.Now;

            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "SUMMERY" })]
        public ActionResult CreateMulti()
        {
            SubmitModel up = new SubmitModel();

            //up.Sources = standingDataService.GetSource().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");
            up.Districts = standingDataService.GetDistricts().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            int i = int.Parse(up.Districts.FirstOrDefault().Value);
            up.Upazillas = standingDataService.GetUpazilla(i).Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.Date = DateTime.Now;

            var s = standingDataService.GetSource().ToList();
            up.MultiDatas = new List<MultiData>();

            foreach(var r in s)
            {
                up.MultiDatas.Add(new MultiData { Id = r.Id, Count = 0, Src=r.Name });
            }
            
            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "SUMMERY" })]
        public ActionResult Edit(int Id)
        {
            try
            {
                var entity = sunDataService.Get(Id);

                SubmitModel up = new SubmitModel();

                up.Sources = standingDataService.GetSource().Where(r => r.IsActive).ToSelectList(up.SourceId, "Id", "Name");
                up.Districts = standingDataService.GetDistricts().Where(r => r.IsActive).ToSelectList(up.DistrictId, "Id", "Name");
                up.Upazillas = standingDataService.GetUpazilla(entity.DistrictId).Where(r => r.IsActive).ToSelectList(up.UpazillaId, "Id", "Name");

                ModelCopier.CopyModel(entity, up);

                return PartialView("Create", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        [CustomAuthorize(Roles: new string[] { "SUMMERY" })]
        public ActionResult Save(SubmitModel model)
        {
            SessionHelper.Temp = model.DistrictId;
            SessionHelper.Temp2 = model.UpazillaId;

            SummerizedData entity = new SummerizedData();
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

        }

        [CustomAuthorize(Roles: new string[] { "SUMMERY" })]
        public ActionResult SaveMulti(SubmitModel model)
        {
            SessionHelper.Temp = model.DistrictId;
            SessionHelper.Temp2 = model.UpazillaId;

            foreach (var v in model.MultiDatas)
            {
                if (v.Count > 0)
                {
                    SummerizedData entity = new SummerizedData();
                    
                    ModelCopier.CopyModel(model, entity);

                    entity.InsertedById = SessionHelper.UserId;
                    entity.SourceId = v.Id;
                    entity.ReachCount = v.Count;
                    entity.CollectedBy = v.CollectedBy;

                    if (entity.Id > 0)
                    {
                        sunDataService.Update(entity);
                    }
                    else
                    {
                        sunDataService.Add(entity);
                    }
                }
            }

            unitOfWork.Commit();

            return PartialView("Save");

        }

        public JsonResult DataGrid()
        {
            int count = 0;
            bool visible = UserRole.Check("SUMMERY", SessionHelper.Role);
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);
            //bool isSum = bool.Parse(Request.QueryString["isSum"]);

            //if (take == -1 || isSum) { take = 1000000000; skip = 0; }

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

            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            List<SummerizedData> dataList = sunDataService.Get(srcId, disId, upzId, FromDate, ToDate, skip, take, out count).ToList();

            //List<SummerizedData> dl = new List<SummerizedData>();

            if (skip<=0 && dataList.Count()>0)
            {
                IEnumerable<SummerizedData> dataListSum = sunDataService.Get(srcId, disId, upzId, FromDate, ToDate, skip, 1000000000, out count);

                SummerizedData tot = dataListSum.GroupBy(q => 1)
                    .Select(g => new SummerizedData
                    {
                        Id = 0
                        ,
                        Date = ToDate
                        ,
                        SourceId = -1
                        ,
                        StandingData = new StandingData { Name = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>" }
                        ,
                        DistrictId = -1
                        ,
                        StandingData1 = new StandingData { Name = " " }
                        ,
                        UpazillaId = -1
                        ,
                        StandingData2 = new StandingData { Name = " " }
                        ,
                        ReachCount = g.Sum(c => c.ReachCount)
                        ,
                        InsertedById=-1
                        ,
                        UserProfile=new UserProfile { UserName="" }
                    }
                    ).Single();

                dataList.Insert(0, tot);
            }
            

            //List<SummerizedData> dataListSOrted = dataList.OrderBy(c => c.StandingData.Name)
            //    .ThenBy(c => c.StandingData1.Name)
            //    .ThenBy(c => c.StandingData2.Name)
            //    .ToList();

            var obj = (from c in dataList
                       select new object[] {c.StandingData.Name
                       ,c.StandingData1.Name
                       ,c.StandingData2.Name                       
                       , String.Format("{0:dd MMM, yyyy}", c.Date)
                       , c.ReachCount 
                       , c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\"", V = (visible && c.Id!=0) }
                    }
            }).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

    }
}
