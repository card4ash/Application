using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Data.Repositories;

namespace AppProj.Data.RepositoriesImpl
{
    public class DistrictByUserProfileRepository: RepositoryBase<DistrictByUserProfile>, IDistrictByUserProfileRepository
    {
        public DistrictByUserProfileRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
