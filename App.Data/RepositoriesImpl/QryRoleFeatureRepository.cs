using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Data;
using AppProj.Domain;

namespace AppProj.Data.RepositoriesImpl
{
    public class QryRoleFeatureRepository : RepositoryBase<QryRoleFeature>, IQryRoleFeatureRepository
    {
        public QryRoleFeatureRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
