using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Data;
using AppProj.Data.Infrastructure;
using System.Data;
using AppProj.Data.Repositories;
using AppProj.Domain;

namespace AppProj.Data.RepositoriesImpl
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }


}
