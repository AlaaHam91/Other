using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using EssentialTools.Models;
namespace EssentialTools.Infrastrucre
{
    public class NinejectDependencyResolver:IDependencyResolver
    {
        private IKernel kernal;
        public NinejectDependencyResolver(IKernel k)
        {
            this.kernal = k;
            AddBindings();
        }

        private void AddBindings() {
           // kernal.Bind<ICalculator>().To<LinqValueCalculator>();
            //   kernal.Bind<IDiscountHelper>().To<Discount>().WithPropertyValue("DiscountSize", 50M);
            kernal.Bind<IDiscountHelper>().To<Discount>().WithConstructorArgument("DiscountSize", 50M);
            //  kernal.Bind<IDiscountHelper>().To<Discount>().WhenInjectedInto<LinqValueCalculator>();
            kernal.Bind<ICalculator>().To<LinqValueCalculator>().InSingletonScope();
        }

        public object GetService(Type serviceType)
        {
            return kernal.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernal.GetAll(serviceType);
        }
    }
}