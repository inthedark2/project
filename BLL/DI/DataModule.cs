using Autofac;
using BLL.Abstract;
using BLL.Concrete;
using DomainModel.Abstract;
using DomainModel.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DI
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            builder.RegisterType<UserProvider>().As<IUserProvider>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
