using AppProj.Domain;
using AppProj.Domain.ModelExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IDetailDataService
    {
        IEnumerable<DetailData> Get();
        DetailData Get(int id);
        IEnumerable<DetailData> Get(int? sourceId, int? disId, int? upzId, DateTime? fromDate, DateTime? toDate,bool onlyPatient, int skp, int tke, out int count);
        IEnumerable<DetailData> GetAppData(int? sourceId, int? disId, int? upzId, DateTime? fromDate, DateTime? toDate, int skp, int tke, out int count);
        DetailData Get(int sourceId, int upzId, DateTime dte);
        void Update(DetailData entity);
        void Add(DetailData entity);
        void Delete(DetailData entity);
        IEnumerable<CountModel> GetThreeIndex();
        IEnumerable<CountModel> GetByAge();
        int GetCount();
        int GetAppCount();
        int GetCountFemale();
        int GetCountFeverBreadth();
        string Vulnarable(int take);

    }
}
