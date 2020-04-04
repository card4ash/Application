using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;


namespace AppProj.Service.Services
{
    public interface IFeatureService
    {
        IEnumerable<Feature> GetFeaturesList();
        Feature GetFeature(int id);
    }
}
