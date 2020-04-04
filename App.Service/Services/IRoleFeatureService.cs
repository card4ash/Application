using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;

namespace AppProj.Service.Services
{
    public interface IRoleFeatureService
    {
        IEnumerable<QryRoleFeature> GetFeaturesByRoleID(int RoleID);
        RoleFeature GetDataByID(int ID);
        void Update(RoleFeature entity);
        void Add(RoleFeature entity);
        void Delete(int ID);
    }
}
