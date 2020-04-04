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
    public class HnppSubmissionController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IHnppDataService sunDataService;
        readonly IUnitOfWork unitOfWork;

        public HnppSubmissionController(IUnitOfWork unitOfWork, IHnppDataService sunDataService, IStandingDataService standingDataService)
        {
            this.standingDataService = standingDataService;
            this.sunDataService = sunDataService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            SearchModel up = new SearchModel();

            var dis = standingDataService.GetDistricts().Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            var upa = standingDataService.GetUpazilla(0).Where(r => r.IsActive);
            up.ContentTypes3 = upa.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        [CustomAuthorize(Roles: new string[] { "HNPP" })]
        public ActionResult Create()
        {
            return PartialView(GetModel());
        }

        [CustomAuthorize(Roles: new string[] { "HNPP" })]
        public ActionResult Edit(int Id)
        {
            try
            {
                var entity = sunDataService.Get(Id);

                SubmitModel up = new SubmitModel();

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

        [CustomAuthorize(Roles: new string[] { "HNPP" })]
        public ActionResult Save(SubmitModel model)
        {
            SessionHelper.Temp = model.DistrictId;
            SessionHelper.Temp2 = model.UpazillaId;
            SessionHelper.Temp3 = model.CollectedBy;

            HnppData entity = new HnppData();

            
            ModelCopier.CopyModel(model, entity);

            entity.InsertedById = SessionHelper.UserId;

            if (entity.Id == 0)
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

        public JsonResult DataGrid()
        {
            int count = 0;
            bool visible = UserRole.Check("HNPP", SessionHelper.Role);
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);
            //bool isSum = bool.Parse(Request.QueryString["isSum"]);

            //if (take == -1 || isSum) { take = 1000000000; skip = 0; }


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

            List<HnppData> dataList = sunDataService.Get(disId, upzId, FromDate, ToDate, skip, take, out count).ToList();
            
            //List<HnppData> dl = new List<HnppData>();

            if (skip<=0 && dataList.Count()>0)
            {
                IEnumerable<HnppData> dataListSum = sunDataService.Get(disId, upzId, FromDate, ToDate, skip, 1000000000, out count);

                HnppData tot = dataListSum.GroupBy(q => 1)
                    .Select(g => new HnppData
                    {
                        Id = 0
                     ,
                        Date = ToDate
                     ,
                        DistrictId = -1
                    ,
                        StandingData = new StandingData { Name = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>" }
                     ,
                        UpazillaId = -1
                    ,
                        StandingData1 = new StandingData { Name = " " }
                     ,
                        RiskByAM = g.Sum(c => c.RiskByAM)
                     ,
                        RiskByPA = g.Sum(c => c.RiskByPA)
                     ,
                        RiskByPK = g.Sum(c => c.RiskByPK)
                     ,
                        RiskBySK = g.Sum(c => c.RiskBySK)
                     ,
                        RiskBySS = g.Sum(c => c.RiskBySS)
                     ,
                        CaseCnt = g.Sum(c => c.CaseCnt)
                     ,
                        BracMeeting = g.Sum(c => c.BracMeeting)
                     ,
                        BracParticipant = g.Sum(c => c.BracParticipant)
                     ,
                        GovtMeeting = g.Sum(c => c.GovtMeeting)
                     ,
                        GovtParticipant = g.Sum(c => c.GovtParticipant)
                    ,
                        Leaflet = g.Sum(c => c.Leaflet)
                    ,
                        Sticker = g.Sum(c => c.Sticker)
                        ,
                        InsertedById = -1
                        ,
                        UserProfile = new UserProfile { UserName = "" }
                    })
                    .Single();

                //dataList.Add(tot);
                dataList.Insert(0, tot);
            }
            
            //List<HnppData> dataListSorted = dataList
            //    .OrderBy(c => c.StandingData.Name)
            //    .ThenBy(c => c.StandingData1.Name)
            //    .ToList();

            var obj = (from c in dataList
                       select new object[] {c.StandingData.Name
                       ,c.StandingData1.Name
                       , String.Format("{0:dd MMM, yyyy}", c.Date)
                       ,c.RiskBySS
                       ,c.RiskBySK
                       ,c.RiskByPK
                       ,c.RiskByPA
                       ,c.RiskByAM
                       ,c.Leaflet
                       ,c.Sticker
                       ,c.CaseCnt
                       ,c.BracMeeting
                       ,c.BracParticipant
                       ,c.GovtMeeting
                       ,c.GovtParticipant
                       ,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\""
                         , V = (visible && c.Id!=0) }
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
            
            SubmitModel up = new SubmitModel();

            up.Districts = standingDataService.GetDistricts().Where(r => r.IsActive).ToSelectList(d, "Id", "Name");

            int i = (d == 0 ? int.Parse(up.Districts.FirstOrDefault().Value) : d);
            up.Upazillas = standingDataService.GetUpazilla(i).Where(r => r.IsActive).ToSelectList(u, "Id", "Name");

            up.Genders = standingDataService.GetGender().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.DistrictId = d;
            up.UpazillaId = u;
            up.Date = DateTime.Now;
            up.CollectedBy = ""+SessionHelper.Temp3;
            
            

            return up;
        }

    }
}
