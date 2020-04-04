using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Data.Repositories;

namespace AppProj.Data.RepositoriesImpl
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
