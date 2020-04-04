using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IStandingDataService
    {
        IEnumerable<StandingData> GetUpazilla(int disId);
        IEnumerable<StandingData> GetSource();
        IEnumerable<StandingData> GetGender();
        IEnumerable<StandingData> GetDistricts();
        IEnumerable<StandingData> GetDistricts(int divId);
        IEnumerable<StandingData> GetDivisions();

        StandingData GetDataById(int id);
        void Add(StandingData entity);
        void Update(StandingData entity);
    }
}
