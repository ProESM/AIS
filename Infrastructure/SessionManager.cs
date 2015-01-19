using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.Attributes;

namespace Infrastructure
{
    public class SessionManager
    {
        private static ISessionFactory Factory { get; set; }
        public static string ConnectionString { get; set; }

        private static Assembly mappingAssembly;
        private static String configResourceName;

        static SessionManager()
        {
            ConnectionString = @"Server=localhost;Database=D:\git\AIS\Database\DB_MAIN.FDB;Charset=UTF8;User=SYSDBA;Password=masterkey";            
            //ConnectionString = @"Server=localhost;Database=C:\inetpub\wwwroot\AIS\Database\DB_MAIN.FDB;Charset=UTF8;User=SYSDBA;Password=masterkey";
        }

        private static ISessionFactory GetFactory<T>() where T : ICurrentSessionContext
        {
            //var cfg = new FirebirdConfiguration().
            //    ConnectionString(ConnectionString).
            //    AdoNetBatchSize(100).
            //    Dialect(typeof(FirebirdDialect).FullName);

            //return Fluently.Configure().Database(cfg).
            //    Mappings(m => m.HbmMappings.AddFromAssembly(typeof(SessionManager).Assembly)).
            //    CurrentSessionContext<T>().
            //    BuildSessionFactory();

            /*mappingAssembly = Assembly.GetExecutingAssembly();

            //configResourceName = "WebMr3.Models.hibernate.cfg.xml";
            configResourceName = "Infrastructure.hibernate.cfg.xml";

            var cfg = new NHibernate.Cfg.Configuration();

            //cfg.Configure();  - работает

            cfg.Configure(mappingAssembly, configResourceName);

            cfg.Properties["connection.connection_string"] = ConnectionString;

            cfg.CurrentSessionContext<T>();*/

            //cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionProvider,
            //    typeof (NHibernate.Connection.DriverConnectionProvider).AssemblyQualifiedName);
            //cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver,
            //    typeof(NHibernate.Driver.FirebirdClientDriver).AssemblyQualifiedName);
            //cfg.SetProperty(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass,
            //    typeof(NHibernate.Bytecode.DefaultProxyFactoryFactory).AssemblyQualifiedName);
            //cfg.SetProperty(NHibernate.Cfg.Environment.Isolation, IsolationLevel.Snapshot.ToString());
            //cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql,
            //    "true");
            //cfg.SetProperty(NHibernate.Cfg.Environment.Dialect,
            //    typeof(NHibernate.Dialect.FirebirdDialect).AssemblyQualifiedName);
            //cfg.SetProperty("use_outer_join",
            //    "true");
            //cfg.SetProperty(NHibernate.Cfg.Environment.CommandTimeout,
            //    "444");
            //cfg.SetProperty(NHibernate.Cfg.Environment.QuerySubstitutions,
            //    "true 1, false 0, yes 1, no 0");            


            /*HbmSerializer.Default.Validate = true;*/

            //cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionProvider,
            //    "NHibernate.Connection.DriverConnectionProvider");            
            //cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.FirebirdDialect");

            //cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.FirebirdClientDriver");
            //cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, ConnectionString);                        

            /*HbmSerializer.Default.HbmAssembly = mappingAssembly.GetName().Name;//FullName;
            HbmSerializer.Default.HbmNamespace = typeof(TreeRepository).Namespace;*/

#if DEBUG
            /*cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");*/
#else
            //cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "false");
#endif

            //using (var f = new FileStream(@"D:\777.xml", FileMode.Append))
            //{
            //    HbmSerializer.Default.Serialize(
            //        mappingAssembly).CopyTo(f);
            //}

            /*cfg.AddInputStream(
                HbmSerializer.Default.Serialize(mappingAssembly));*/

            //new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(true, true);

            var cfg = new FirebirdConfiguration().
                ConnectionString(ConnectionString).
                AdoNetBatchSize(100).
                Dialect(typeof(FirebirdDialect).FullName);

#if DEBUG
            cfg.ShowSql();
#else
            //cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "false");
#endif

            return Fluently.Configure().Database(cfg).                
                Mappings(m => m.FluentMappings.AddFromAssembly(typeof(UserDao).Assembly)).
                CurrentSessionContext<T>().
                BuildSessionFactory();

            /*return cfg.BuildSessionFactory();*/
        }

        public static ISession CurrentSession
        {
            get
            {
                if (Factory == null)
                    Factory = HttpContext.Current != null
                        ? GetFactory<WebSessionContext>()
                        : GetFactory<ThreadStaticSessionContext>();
                if (CurrentSessionContext.HasBind(Factory))
                    return Factory.GetCurrentSession();
                ISession session = Factory.OpenSession();
                CurrentSessionContext.Bind(session);
                return session;
            }
        }

        public static void CloseSession()
        {
            if (Factory == null)
                return;
            if (CurrentSessionContext.HasBind(Factory))
            {
                ISession session = CurrentSessionContext.Unbind(Factory);
                session.Close();
            }
        }

        public static void CommitSession(ISession session)
        {
            try
            {
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
        }
    }
}
