using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignition.Data
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Reflection;
    using Common.Data;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Tool.hbm2ddl;
    //using SunGardPS.Common.Data;

    /// <summary>
    /// The fluent-helper for common tasks
    /// </summary>
    public class FluentHelper : NHibernateHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentHelper"/> class.
        /// </summary>
        public FluentHelper()
            : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentHelper"/> class.
        /// </summary>
        /// <param name="connectionKey">The connection key.</param>
        /// <param name="autoConfig">if set to <c>true</c> [auto config].</param>
        public FluentHelper(string connectionKey, bool autoConfig = false)
            : this(
                () =>
                Fluently.Configure()
                .Database(GetConfigurationOption(connectionKey))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                    .ExposeConfiguration((c =>
                    {
                        if (autoConfig)
                        {
                            BuildSchema(c);
                        }
                    })).BuildSessionFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentHelper"/> class.
        /// </summary>
        /// <param name="autoConfig">if set to <c>true</c> [auto config].</param>
        /// <remarks>Used primarily for unit-testing ONLY! All others should use connection key.</remarks>
        public FluentHelper(bool autoConfig)
            : this(() => Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2000.ConnectionString(
                                c => c.Server(@".\sqlexpress")
                                        .Database("WebUpdater")
                                        .TrustedConnection()))
                            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                            .ExposeConfiguration(c =>
                            {
                                if (autoConfig)
                                {
                                    BuildSchema(c);
                                }
                            })
                            .BuildSessionFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentHelper"/> class.
        /// </summary>
        /// <param name="sessionMethod">The session method.</param>
        public FluentHelper(Func<ISessionFactory> sessionMethod)
            : base(sessionMethod)
        {
        }

        /// <summary>
        /// Gets the configuration option.
        /// References http://www.carlprothman.net/Default.aspx?tabid=87
        /// http://www.devlist.com/ConnectionStringsPage.aspx
        /// </summary>
        /// <param name="connectionKey">The connection key.</param>
        /// <returns>The PersistanceConfigurator</returns>
        private static IPersistenceConfigurer GetConfigurationOption(string connectionKey)
        {
            string connection = ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString;

            switch (ConfigurationManager.ConnectionStrings[connectionKey].ProviderName)
            {
                case "System.Data.SqlClient":
                    return MsSqlConfiguration.MsSql2000.ConnectionString(connection);

                case "System.Data.OracleClient":
                    return OracleDataClientConfiguration.Oracle9.ConnectionString(connection);

                case "System.Data.OleDb":
                    {
                        var c = new OleDbConnectionStringBuilder(connection);
                        switch (c.Provider)
                        {
                            case "DB2OLEDB":
                                return DB2Configuration.Standard.ConnectionString(connection);
                            case "MySQLProv":
                                return MySQLConfiguration.Standard.ConnectionString(connection);
                            case "msdaora":
                            case "OraOLEDB.Oracle":
                                return OracleClientConfiguration.Oracle9.ConnectionString(connection);
                            case "sqloledb":
                                return MsSqlConfiguration.MsSql2000.ConnectionString(connection);
                            case "PostgreSQL OLE DB Provider":
                                return PostgreSQLConfiguration.Standard.ConnectionString(connection);
                            default:
                                return MsSqlConfiguration.MsSql2000.ConnectionString(connection);
                        }
                    }

                case "System.Data.Odbc":
                    {
                        var c = new System.Data.Odbc.OdbcConnectionStringBuilder(connection);
                        switch (c.Driver)
                        {
                            case "{IBM DB2 ODBC DRIVER}":
                                return DB2Configuration.Standard.ConnectionString(connection);
                            case "{MySQL ODBC 3.51 Driver}":
                                return MySQLConfiguration.Standard.ConnectionString(connection);
                            case "{Microsoft ODBC for Oracle}":
                                return OracleClientConfiguration.Oracle9.ConnectionString(connection);
                            case "{SQL Server}":
                                return MsSqlConfiguration.MsSql2000.ConnectionString(connection);
                            case "{PostgreSQL}":
                                return PostgreSQLConfiguration.Standard.ConnectionString(connection);
                            default:
                                return MsSqlConfiguration.MsSql2000.ConnectionString(connection);
                        }
                    }

                default:
                    return MsSqlConfiguration.MsSql2000.ConnectionString(connection);
            }
        }

        /// <summary>
        /// Builds the custom schema.
        /// </summary>
        /// <param name="cfg">The internal configuration.</param>
        private static void BuildSchema(NHibernate.Cfg.Configuration cfg)
        {
            const string databaseToken = "{7EEE6F1E-C952-4674-96E1-E4DCB35FB698}";
            string databaseScript;
            new SchemaExport(cfg).Create(false, true);
            try
            {
                if (cfg.Properties.ContainsKey("hibernate.dialect"))
                {
                    var dialect = cfg.Properties["hibernate.dialect"];
                    if (dialect.Contains("MsSql"))
                    {
                        if (cfg.Properties.ContainsKey("connection.connection_string"))
                        {
                            var splitter = new[] { "\r\nGO\r\n" };

                            #region AutoMagic DB creation
                            databaseScript = string.Empty;
                            //databaseScript = SunGardPS.WebUpdater.Service.Data.Scripts.SQLServer.Replace(
                            //    databaseToken,
                            //    GetCatalogName(
                            //        new System.Data.SqlClient.SqlConnectionStringBuilder(
                            //            cfg.Properties["connection.connection_string"])));
                            string[] commandTexts = databaseScript.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                            #endregion

                            // only auto-creating for sql server at this point...could return different "type" of connection for each db to support...
                            using (var connection = new SqlConnection(cfg.Properties["connection.connection_string"]))
                            {
                                if (connection.State != ConnectionState.Open)
                                {
                                    connection.Open();
                                }

                                foreach (string commandText in commandTexts)
                                {
                                    Console.WriteLine(commandText);
                                    using (var cmd = connection.CreateCommand())
                                    {
                                        cmd.CommandText = commandText;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            // execute "custom" scripts here!
        }

        /// <summary>
        /// Gets the name of the catalog.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The database or catalog name</returns>
        private static string GetCatalogName(System.Data.Common.DbConnectionStringBuilder builder)
        {
            return builder.ContainsKey("Initial Catalog")
                       ? builder["Initial Catalog"].ToString()
                       : (builder.ContainsKey("Database")
                       ? builder["Database"].ToString() : string.Empty);
        }
    }
}
