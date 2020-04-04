using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Service.Services;
using AppProj.Data.Repositories;
using AppProj.Data.Infrastructure;
using AppProj.Domain;

namespace AppProj.Service.ServicesImpl
{
    public class DistrictByUserProfileService : IDistrictByUserProfileService
    {
        readonly IDistrictByUserProfileRepository disByUserRepository;
        readonly IUnitOfWork unitOfWork;

        public DistrictByUserProfileService(IDistrictByUserProfileRepository disByUserRepository, IUnitOfWork unitOfWork)
        {
            this.disByUserRepository = disByUserRepository;
            this.unitOfWork = unitOfWork;
        }

        public DistrictByUserProfile Get(int Id)
        {
            return disByUserRepository.Get(c => c.Id == Id);
        }

        public IEnumerable<DistrictByUserProfile> GetByUser(int userId)
        {
            return disByUserRepository.GetMany(c => c.UserInfoId == userId);
        }

        public void Add(DistrictByUserProfile entity)
        {
            disByUserRepository.Add(entity);
        }

        public void Delete(DistrictByUserProfile entity)
        {
            disByUserRepository.Delete(entity);
        }

    }
}
