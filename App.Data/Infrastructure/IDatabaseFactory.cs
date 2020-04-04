using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppProj.Domain;

namespace AppProj.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        AppModelContainer Get();        
    }
}
