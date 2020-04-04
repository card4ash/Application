using AppProj.Domain;
using AppProj.Domain.ModelExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface ISummerizedDataService
    {
        IEnumerable<SummerizedData> Get();
        SummerizedData Get(int id);
        IEnumerable<SummerizedData> Get(int? sourceId, int? disId, int? upzId, DateTime? fromDate,DateTime? toDate, int skip, int take, out int count);
        SummerizedData Get(int sourceId, int upzId, DateTime dte);
        void Update(SummerizedData entity);
        void Add(SummerizedData entity);
        void Delete(SummerizedData entity);
        IEnumerable<CountModel> GetBySource();
        IEnumerable<CountModel> GetByDistrict();
        int GetReachCount();

    }
}
