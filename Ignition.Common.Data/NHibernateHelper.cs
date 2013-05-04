
namespace Ignition.Common.Data
{
    using NHibernate;
    using System;

    /// <summary>
    /// Implementation of the INHibernateHelper
    /// </summary>
    public abstract class NHibernateHelper : INHibernateHelper
    {
        /// <summary>
        /// The factory's factory method
        /// </summary>
        private readonly Func<ISessionFactory> sessionFactoryMethod;

        /// <summary>
        /// The registered Session Factory
        /// </summary>
        private ISessionFactory sessionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateHelper"/> class.
        /// </summary>
        /// <param name="sessionMethod">The session method.</param>
        protected NHibernateHelper(Func<ISessionFactory> sessionMethod)
        {
            this.sessionFactoryMethod = sessionMethod;
        }

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get { return this.sessionFactory ?? (this.sessionFactory = this.CreateSessionFactory()); }
        }

        /// <summary>
        /// The main method for spinning up a session instance.
        /// </summary>
        /// <returns>
        /// ISessionFactory implementation
        /// </returns>
        public ISessionFactory CreateSessionFactory()
        {
            return this.sessionFactoryMethod != null ? this.sessionFactoryMethod.Invoke() : null;
        }
    }
}
