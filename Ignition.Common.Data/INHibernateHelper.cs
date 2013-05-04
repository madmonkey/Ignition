
namespace Ignition.Common.Data
{
    using NHibernate;

    /// <summary>SessionFactory interface</summary>
    public interface INHibernateHelper
    {
        /// <summary>The main method for spinning up a session instance.</summary>
        /// <returns>ISessionFactory implementation</returns>
        ISessionFactory CreateSessionFactory();
    }
}
