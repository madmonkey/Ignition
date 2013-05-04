
namespace Ignition.Common.Data
{
    using System;
    using NHibernate;

    /// <summary>
    /// The Unit Of Work interface
    /// </summary>
    /// <remarks>Refer to: http://martinfowler.com/eaaCatalog/unitOfWork.html</remarks>
    /// <see cref="http://msdn.microsoft.com/en-us/magazine/dd882510.aspx"/>
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/aa973811.aspx"/>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the session.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();
    }
}
