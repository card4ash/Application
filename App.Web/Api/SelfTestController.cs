using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Service.Services;
using AppProj.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Api
{
    public class SelfTestController : Controller
    {

        readonly IDetailDataService sunDataService;
        readonly IUnitOfWork unitOfWork;

        public SelfTestController(IUnitOfWork unitOfWork, IDetailDataService sunDataService)
        {
            this.sunDataService = sunDataService;
            this.unitOfWork = unitOfWork;

        }

        [HttpPost]
        public JsonResult Index(ApiModel model)
        {
            if(model.Key.Trim() != "9AF62612-CC13-46FE-8906-96E60DCC34D1")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DetailData entity = new DetailData();

                entity.SourceId = 860538;
                entity.DistrictId = 860539;
                entity.UpazillaId = 860540;
                entity.Date = DateTime.Now.Date;
                entity.Phone = model.Phone;
                entity.CollectedBy = model.PIN;
                entity.IsFever = model.HasFever;
                entity.IsBreadth = model.HasBreathingProblem;
                entity.IsDryCough = model.HasDryCough;
                entity.Description = model.Address;

                if (model.Gender.ToLower() == "male")
                {
                    entity.GenderId = 860516;
                }
                else if (model.Gender.ToLower() == "female")
                {
                    entity.GenderId = 860517;
                }

                entity.IsContact = model.HasAnyContactWithSuspected;

                entity.IsRespiratory = false;
                entity.Name = "";
                entity.InsertedById = 333;
                
                sunDataService.Add(entity);
                unitOfWork.Commit();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            
        }

    }
}
