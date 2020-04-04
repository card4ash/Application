using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IDistrictDataService
    {
        DistrictData Get(int districtId, DateTime date);
        DistrictSummery GetSummery(int districtId);
        IEnumerable<DistrictData> Get(int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count);
        IEnumerable<DistrictSummery> GetSummery(int? divId, int? disId, int skip, int take, out int count);
        IEnumerable<DistrictSummery> GetSummery();
        void Update(DistrictData disEntity, DistrictSummery sumEntity);

    }
}
