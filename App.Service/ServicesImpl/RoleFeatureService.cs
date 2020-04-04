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
    public class RoleFeatureService : IRoleFeatureService
    {
        readonly IRoleFeatureRepository roleFeatureRepository;
        readonly IQryRoleFeatureRepository qryRoleFeatureRepository;
        
        public RoleFeatureService(IRoleFeatureRepository roleFeatureRepository,IQryRoleFeatureRepository qryRoleFeatureRepository)
        {
            this.roleFeatureRepository = roleFeatureRepository;
            this.qryRoleFeatureRepository = qryRoleFeatureRepository;
        }

        public IEnumerable<QryRoleFeature> GetFeaturesByRoleID(int roleID)
        {
            return qryRoleFeatureRepository.GetMany(r => r.RoleId == roleID);
        }
        public RoleFeature GetDataByID(int ID)
        {
            return roleFeatureRepository.GetById(ID);
        }
        public void Update(RoleFeature entity)
        {
            roleFeatureRepository.Update(entity);            
        }
        public void Add(RoleFeature entity)
        {
            roleFeatureRepository.Add(entity);            
        }
        public void Delete(int ID)
        {
            roleFeatureRepository.Delete(roleFeatureRepository.GetById(ID));         
        }        
    }
}
