using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IUserLoginLogService
    {
        void Add(UserLoginLog entity);
    }
}
