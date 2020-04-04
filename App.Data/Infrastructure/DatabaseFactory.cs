using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppProj.Domain;

namespace AppProj.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private AppModelContainer dataContext;
        public AppModelContainer Get()
        {
            return dataContext ?? (dataContext = new AppModelContainer());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
