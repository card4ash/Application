using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;

namespace AppProj.Service.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetDataById(int id);
        void Add(Role entity);
        void Update(Role entity);        
    }
}
