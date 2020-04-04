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
    public class FeatureService : IFeatureService
    {
        readonly IFeatureRepository featureRepository;
        readonly IUnitOfWork unitOfWork;

        public FeatureService(IFeatureRepository featureRepository, IUnitOfWork unitOfWork)
        {
            this.featureRepository = featureRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IFeatureService Members

        public IEnumerable<Feature> GetFeaturesList()
        {
            var features = featureRepository.GetAll();
            return features;
        }

        public Feature GetFeature(int id)
        {
            var features = featureRepository.GetById(id);
            return features;
        }

        #endregion
    }
}
