using System;

namespace Ignition
{
    using Contracts;
    using Contracts.DataContracts;
    using ServiceStack.CacheAccess.Providers;
    using Services;
    using ServiceStack.CacheAccess;
    using ServiceStack.Configuration;
    using ServiceStack.Logging;
    using ServiceStack.Logging.Support.Logging;
    using ServiceStack.Redis;
    using ServiceStack.WebHost.Endpoints;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using ISessionFactory = NHibernate.ISessionFactory;

    //using Ignition.Contracts;

    public class Global : System.Web.HttpApplication
    {
        private const string dbName = "Ignition";

        protected void Application_Start(object sender, EventArgs e)
        {
            var fh = new Ignition.Data.FluentHelper(dbName, CheckForTablesExist());
            (new IgnitionServiceAppHost(fh.CreateSessionFactory())).Init();
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public class IgnitionServiceAppHost : AppHostBase
        {
#pragma warning disable 649
            private readonly IContainerAdapter _containerAdapter;
#pragma warning restore 649
            public IgnitionServiceAppHost(ISessionFactory sessionFactory)
                : base("Ignition Sample", typeof(CompanyService).Assembly)
            {
                LogManager.LogFactory = new ConsoleLogFactory(); //<-can be swapped later
                base.Container.Register<ICacheClient>(new MemoryCacheClient());//<-can also be swapped later to cache-server
                base.Container.Register(sessionFactory);
            }

            public override void Configure(Funq.Container container)
            {
                ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
                container.Adapter = _containerAdapter;
                Routes.Add<Company>("/companies").Add<Company>("/companies/{Id}").Add<Company>("/companies/{Name}");
                Routes.Add<SummaryCategory>("/categories");
                Routes.Add<SummaryLocation>("/locations");
            }
        }

        private static bool CheckForTablesExist()
        {
            int count = 1000;
            try
            {
                const string command = "SELECT COUNT(*) from information_schema.tables WHERE table_type = 'base table'";
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var cmd = new SqlCommand(command, connection);
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //LoggingService.Error(ex);
            }

            return count == 0;
        }

    }
}