using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.IOC
{
    public class CustomControllerActivator : IControllerActivator
    {
        IController IControllerActivator.Create(
            System.Web.Routing.RequestContext requestContext,
            Type controllerType)
        {
            return DependencyResolver.Current
                .GetService(controllerType) as IController;
        }
    }
}