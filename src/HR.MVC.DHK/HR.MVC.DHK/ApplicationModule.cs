using Autofac;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using HR.MVC.DHK.RepositoryPattern.Repositories;

namespace HR.MVC.DHK
{
    public class ApplicationModule : Module
    {

        public ApplicationModule()
        {

        }



        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
