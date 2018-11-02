using Autofac;
using System.Reflection;

namespace DGTWebApi
{
    public class DIConfig:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("DGT.Services"))
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("DGT.Manager"))
                   .Where(t => t.Name.Equals("DGTManager"))
                  .SingleInstance();
        }
    }
}