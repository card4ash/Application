﻿using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Data.RepositoriesImpl
{
    public class DetailDataRepository : RepositoryBase<DetailData>, IDetailDataRepository
    {
        public DetailDataRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
