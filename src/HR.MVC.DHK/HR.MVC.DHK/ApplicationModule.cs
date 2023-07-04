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

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DesignationRepository>().As<IDesignationRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ShiftRepository>().As<IShiftRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AttendanceRepository>().As<IAttendanceRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ReportRepository>().As<IReportRepository>()
                   .InstancePerLifetimeScope();
            //builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
