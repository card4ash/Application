using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IHnppDataService
    {
        IEnumerable<HnppData> Get();
        HnppData Get(int id);
        IEnumerable<HnppData> Get(int? disId, int? upzId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count);
        HnppData Get(int upzId, DateTime dte);
        void Update(HnppData entity);
        void Add(HnppData entity);
        void Delete(HnppData entity);
        int GetCount();
    }
}
