using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using AppProj.Domain.ModelExt;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class DetailDataService : IDetailDataService
    {
        readonly IDetailDataRepository repository;
        readonly IUnitOfWork unitOfWork;

        public DetailDataService(IDetailDataRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DetailData> Get()
        {
            return repository.GetMany(c => c.SourceId!= 860538 && 
            (c.IsContact == true
                || (c.IsFever == true && c.IsBreadth == true)
                ));
        }
        public DetailData Get(int id)
        {
            return repository.GetById(id);
        }
        public IEnumerable<DetailData> Get(int? sourceId, int? disId, int? upzId, DateTime? fromDate, DateTime? toDate, bool onlyPatient, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            IEnumerable<DetailData> data = repository.GetMany
                (c => c.SourceId != 860538 && c.Date >= fromDate && c.Date <= toDate
                && (onlyPatient ? (c.IsContact == true
                        || (c.IsFever == true && c.IsBreadth == true)
                   ) : true)
                && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.StandingData2.Name)
                .ThenBy(c => c.Date)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount(c => c.SourceId != 860538 && c.Date >= fromDate && c.Date <= toDate
                && (onlyPatient ? (c.IsContact == true
                        || (c.IsFever == true && c.IsBreadth == true)
                   ) : true)
                && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                );

            return data;
        }
        public IEnumerable<DetailData> GetAppData(int? sourceId, int? disId, int? upzId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            IEnumerable<DetailData> data = repository.GetMany
                (c => c.SourceId == 860538 && c.Date >= fromDate && c.Date <= toDate                
                && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.StandingData2.Name)
                .ThenBy(c => c.Date)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount(c => c.SourceId == 860538 && c.Date >= fromDate && c.Date <= toDate
                
                && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                );

            return data;
        }
        public DetailData Get(int sourceId, int upzId, DateTime dte)
        {
            return repository.GetMany(c => c.SourceId == sourceId && c.Date == dte && c.UpazillaId == upzId)
                .FirstOrDefault();
        }
        public void Update(DetailData entity)
        {
            repository.Update(entity);
        }
        public void Add(DetailData entity)
        {
            repository.Add(entity);
        }
        public void Delete(DetailData entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<CountModel> GetThreeIndex()
        {
            return repository.GetMany(c=> c.SourceId != 860538)
                .GroupBy(l =>
                l.StandingData1.Name)
               .Select(c => new CountModel
               {
                   Name = c.Key
                   ,
                   Count = c.Count(d => d.IsFever == true && d.IsBreadth == true)
                   ,
                   Count2 = c.Count(d => d.IsContact == true)
               })
               .Where(c => c.Count > 0 || c.Count2 > 0);
        }

        public IEnumerable<CountModel> GetByAge()
        {
            return repository.GetMany(c => c.SourceId != 860538 &&
                    (c.IsContact == true
                || (c.IsFever == true && c.IsBreadth == true)))
                .GroupBy(l =>
                (l.Age < 30 ? "৩০ এর কম" : l.Age >= 30 && l.Age < 70 ? "৩০ থেকে ৬৯" : "৭০ এবং তদুর্ধ"))
               .Select(c =>
               new CountModel
               {
                   Name = c.Key
               ,
                   Count = c.Count()
               })
               .Where(c => c.Count > 0); ;
        }

        public int GetAppCount()
        {
            return repository.GetCount(c => c.SourceId == 860538);
        }
        public int GetCount()
        {
            return repository.GetCount(c => c.SourceId != 860538 &&
                   ( c.IsContact == true
                || (c.IsFever == true && c.IsBreadth == true)));
        }

        public int GetCountFemale()
        {
            return repository.GetCount(c => c.SourceId != 860538 &&
                ((c.IsContact == true
                || (c.IsFever == true && c.IsBreadth == true)) 
                && c.GenderId == 860517));
        }

        public int GetCountFeverBreadth()
        {
            return repository.GetCount(c => c.SourceId != 860538 &&
            c.IsFever == true && c.IsBreadth == true);
        }
        
        public string Vulnarable(int take)
        {
            IEnumerable<CountModel> model = repository.GetMany(c=> c.SourceId != 860538)
                .GroupBy(l =>
                l.StandingData1.Name)
               .Select(c => new CountModel
               {
                   Name = c.Key
                   ,
                   Count = c.Count(d => 
                   (d.IsFever == true && d.IsBreadth == true) || d.IsContact == true)                 
               })
               .OrderByDescending(c=> c.Count)
               .Take(take)
               .Skip(0);

            return string.Join(", ", model.Select(c=>c.Name).ToArray());

        }
    }
}
