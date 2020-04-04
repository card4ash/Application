using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;

namespace AppProj.Service.Services
{
    public interface IDistrictByUserProfileService
    {
        DistrictByUserProfile Get(int Id);
        IEnumerable<DistrictByUserProfile> GetByUser(int userId);
        void Add(DistrictByUserProfile entity);
        void Delete(DistrictByUserProfile entity);
    }
}
