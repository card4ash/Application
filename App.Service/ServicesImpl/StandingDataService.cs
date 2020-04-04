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
    public class StandingDataService: IStandingDataService
    {
        readonly IStandingDataRepository serviceRepository;
        readonly IUnitOfWork unitOfWork;

        public StandingDataService(IStandingDataRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            this.serviceRepository = serviceRepository;
            this.unitOfWork = unitOfWork;
        }


        public IEnumerable<StandingData> GetUpazilla(int disId)
        {
            return serviceRepository
                .GetMany(c => c.Type == "UPZ" && c.ParentId==disId && c.IsActive)
                .OrderBy(d=> d.Name);
        }

        public IEnumerable<StandingData> GetSource()
        {
            return serviceRepository
                .GetMany(c => c.Type == "SRC" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetGender()
        {
            return serviceRepository
                .GetMany(c => c.Type == "GEN" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetDistricts()
        {
            return serviceRepository
                .GetMany(c => c.Type == "DIS" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetDistricts(int divId)
        {
            return serviceRepository
                .GetMany(c => c.Type == "DIS" && c.IsActive && c.ParentId==divId)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetDivisions()
        {
            return serviceRepository
                .GetMany(c => c.Type == "DIV" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public StandingData GetDataById(int id)
        {
            return serviceRepository.GetById(id);
        }
        public void Add(StandingData entity)
        {
            serviceRepository.Add(entity);
        }
        public void Update(StandingData entity)
        {
            serviceRepository.Update(entity);
        }
    }
}
