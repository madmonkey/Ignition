
namespace Ignition.Web
{
    using Contracts;
    using ServiceStack.MiniProfiler;
    using Services;
    using ServiceStack.CacheAccess;
    using ServiceStack.CacheAccess.Providers;
    using ServiceStack.Configuration;
    using ServiceStack.Logging;
    using ServiceStack.Logging.Support.Logging;
    using ServiceStack.Razor;
    using ServiceStack.WebHost.Endpoints;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net;
    using ISessionFactory = NHibernate.ISessionFactory;

    /// <summary>
    /// The global base
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// The db name
        /// </summary>
        private const string dbName = "Ignition";

        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            var fh = new Ignition.Data.FluentHelper(dbName, CheckForTablesExist());
            (new IgnitionServiceAppHost(fh.CreateSessionFactory())).Init();

        }

        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.IsLocal)
                Profiler.Start();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Profiler.Stop();
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application host
        /// </summary>
        public class IgnitionServiceAppHost : AppHostBase
        {
#pragma warning disable 649
            private readonly IContainerAdapter _containerAdapter;
#pragma warning restore 649
            public IgnitionServiceAppHost(ISessionFactory sessionFactory)
                : base("Ignition Sample", typeof(CompanyService).Assembly)
            {
                LogManager.LogFactory = new ConsoleLogFactory(); //<-can be swapped later
                base.Container.Register<ICacheClient>(new MemoryCacheClient());//<-can also be swapped later to cache-server (redis, whatever)
                base.Container.Register(sessionFactory);
            }

            /// <summary>
            /// Configures the specified container.
            /// </summary>
            /// <param name="container">The container.</param>
            public override void Configure(Funq.Container container)
            {
                ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
                container.Adapter = _containerAdapter;
                Plugins.Add(new RazorFormat());
                SetConfig(new EndpointHostConfig {
                CustomHttpHandlers = {
                    { HttpStatusCode.NotFound, new RazorHandler("/notfound") }
                }
            });
                Routes.Add<Company>("/companies").Add<Company>("/companies/{id}").Add<Company>("/companies/{name}");
                Routes.Add<Summary>("/summaries");
                Routes.Add<Audit>("/audit").Add<Audit>("/audit/{pg}{limit}");
            }
        }

        /// <summary>
        /// Checks if the required tables exist.
        /// </summary>
        /// <returns></returns>
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
                LogManager.GetLogger("").Error(ex);
            }

            return count == 0;
        }

    }
}