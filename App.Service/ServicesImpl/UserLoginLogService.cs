using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class UserLoginLogService : IUserLoginLogService
    {
        readonly IUserLoginLogRepository repository;
        readonly IUnitOfWork unitOfWork;

        public UserLoginLogService(IUserLoginLogRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(UserLoginLog entity)
        {
            repository.Add(entity);
        }
    }
}
