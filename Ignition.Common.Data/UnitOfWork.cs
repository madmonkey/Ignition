
namespace Ignition.Common.Data
{
    using NHibernate;
    using System;
    using System.Data;

    /// <summary>
    /// The implementation of IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The ISessionFactory
        /// </summary>
        private readonly ISessionFactory sessionFactory;

        /// <summary>
        /// The ITransaction interface
        /// </summary>
        private readonly ITransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public UnitOfWork(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            this.Session = this.sessionFactory.OpenSession();
            this.Session.FlushMode = FlushMode.Auto;
            this.transaction = this.Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public UnitOfWork(ISession session)
        {
            this.Session = session;
            this.Session.FlushMode = FlushMode.Auto;
            this.transaction = this.Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Session.Close();
        }

        /// <summary>Commits the current transaction if applicable</summary>
        /// <exception cref="InvalidOperationException">Thrown if the transaction is not currently active.</exception>
        public void Commit()
        {
            if (!this.transaction.IsActive)
            {
                throw new InvalidOperationException("No active transaction");
            }

            this.transaction.Commit();
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            if (this.transaction.IsActive)
            {
                this.transaction.Rollback();
            }
        }
    }
}
