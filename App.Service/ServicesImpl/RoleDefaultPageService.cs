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
    public class RoleDefaultPageService : IRoleDefaultPageService
    {
        readonly IRoleDefaultPageRepository defaultRepository;
        readonly IUnitOfWork unitOfWork;

        public RoleDefaultPageService(IRoleDefaultPageRepository defaultRepository, IUnitOfWork unitOfWork)
        {
            this.defaultRepository = defaultRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IRoleDefaultPageService Members

        public IEnumerable<RoleDefaultPage> GetRoleDefaultPageList()
        {
            var features = defaultRepository.GetAll();
            return features;
        }

        public RoleDefaultPage GetRoleDefaultPage(int id)
        {
            var features = defaultRepository.GetById(id);
            return features;
        }

        #endregion
    }
}
