using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Data.Repositories;
using AppProj.Data.Infrastructure;
using AppProj.Domain;

namespace AppProj.Data.RepositoriesImpl
{
    public class RoleFeatureRepository : RepositoryBase<RoleFeature>, IRoleFeatureRepository
    {
        public RoleFeatureRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
