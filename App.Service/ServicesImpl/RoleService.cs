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
    public class RoleService : IRoleService
    {
        readonly IRoleRepository roleRepository;
                
        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;            
        }

        public IEnumerable<Role> GetAll()
        {
            return roleRepository.GetAll();
        }
        public Role GetDataById(int id)
        {
            return roleRepository.GetById(id);
        }
        public void Add(Role entity)
        {
            roleRepository.Add(entity);
            
        }
        public void Update(Role entity)
        {
            roleRepository.Update(entity);
        }

    }
}
