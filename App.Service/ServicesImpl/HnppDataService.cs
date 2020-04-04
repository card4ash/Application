using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class HnppDataService : IHnppDataService
    {
        readonly IHnppDataRepository repository;
        readonly IUnitOfWork unitOfWork;

        public HnppDataService(IHnppDataRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<HnppData> Get()
        {
            return repository.GetAll();
        }
        public HnppData Get(int id)
        {
            return repository.GetById(id);
        }
        public IEnumerable<HnppData> Get(int? disId, int? upzId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            IEnumerable<HnppData> data = repository.GetMany(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.Date)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (disId == null ? true : c.DistrictId == disId)
                && (upzId == null ? true : c.UpazillaId == upzId)
                );

            return data;
        }
        public HnppData Get(int upzId, DateTime dte)
        {
            return repository.GetMany(c => c.Date == dte && c.UpazillaId == upzId)
                .FirstOrDefault();
        }
        public void Update(HnppData entity)
        {
            repository.Update(entity);
        }
        public void Add(HnppData entity)
        {
            repository.Add(entity);
        }
        public void Delete(HnppData entity)
        {
            repository.Delete(entity);
        }

        public int GetCount()
        {
            return repository.GetCount();
        }
    }
}
