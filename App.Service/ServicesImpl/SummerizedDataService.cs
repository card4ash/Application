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
    public class SummerizedDataService : ISummerizedDataService
    {
        readonly ISummerizedDataRepository repository;
        readonly IUnitOfWork unitOfWork;

        public SummerizedDataService(ISummerizedDataRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<SummerizedData> Get()
        {
            return repository.GetAll();
        }
        public SummerizedData Get(int id)
        {
            return repository.GetById(id);
        }
        public IEnumerable<SummerizedData> Get(int? sourceId, int? disId, int? upzId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            IEnumerable<SummerizedData> data = repository.GetMany(c => 
            c.Date >= fromDate && c.Date <= toDate
            && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.StandingData2.Name)
                .ThenBy(c => c.Date)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                );

            return data;
        }

        public SummerizedData Get(int sourceId, int upzId, DateTime dte)
        {
            return repository.GetMany(c => c.SourceId == sourceId && c.UpazillaId == upzId && c.Date == dte)
                .FirstOrDefault();
        }

        public void Update(SummerizedData entity)
        {
            repository.Update(entity);
        }
        public void Add(SummerizedData entity)
        {
            repository.Add(entity);
        }
        public void Delete(SummerizedData entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<CountModel> GetBySource()
        {
            return repository.GetAll()
                .GroupBy(l => l.StandingData.Name)
                .Select(c => new CountModel { Name = c.Key, Count = c.Sum(d => d.ReachCount) });
        }

        public IEnumerable<CountModel> GetByDistrict()
        {
            return repository.GetAll()
                .GroupBy(l => l.StandingData1.Name)
               .Select(c => new CountModel { Name = c.Key, Count = c.Sum(d => d.ReachCount) });
        }

        public int GetCount()
        {
            return repository.GetCount();
        }

        public int GetReachCount()
        {
            return repository.GetAll().Sum(c => c.ReachCount);
        }
    }
}
