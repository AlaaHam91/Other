
using BookStore.Domain.Abstract;
using BookStore.Domain.Entity;
using BookStore.Domain.Concerate;

using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using BookStore.WebUI.Infrastructure.Abstract;
using BookStore.WebUI.Infrastructure.Concreate;

namespace BookStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernal;

        public NinjectDependencyResolver(IKernel kernalparam)
        {

            mykernal = kernalparam;
            AddBindings();
        }

        private void AddBindings()
        {
            Mock<IBookRepository> mymock = new Mock<IBookRepository>();
            //mymock.Setup(b => b.Books).Returns(
            //    new List<Book> {

            //        new Book { ISBN=  1,Title="Book1",Price=250,Description="This book for beginer",Specizailation="IT" },
            //        new Book { ISBN = 2, Title = "Book2" ,Price=20,Description="This book for IT",Specizailation="IR"},
            //        new Book { ISBN = 3, Title = "Book3" ,Price=400,Description="This book for SW",Specizailation="IT"},
            //        new Book { ISBN = 4, Title = "Book4" ,Price=255,Description="This book for RT",Specizailation="DER" },
            //        new Book { ISBN = 5, Title = "Book5" ,Price=275,Description="This book for FN",Specizailation="SS" }
            //    }

            //    );

            EmailSettings emailsettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")

            };
         //  mykernal.Bind<IBookRepository>().ToConstant(mymock.Object);

          mykernal.Bind<IBookRepository>().To<EFBookRepository>();
            mykernal.Bind<IAuthenticationProvider>().To<FormsAuthProvider>();
          mykernal.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("setting", emailsettings);
        }

        public object GetService(Type serviceType)
        {
            return mykernal.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return mykernal.GetAll(serviceType);
        }

    }
}