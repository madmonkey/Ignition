
namespace Ignition.Common.Data
{
    using NHibernate;
    using NHibernate.Engine;
    using NHibernate.Linq;
    using System.Linq;

    /// <summary>
    /// The implementation of the IReadOnlyRepository
    /// </summary>
    /// <typeparam name="T">Entity used by NHibernate -> remember all properties MUST be declared as virtual - so a proxy can be generated.</typeparam>
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// The internal linq-implementation.
        /// </summary>
        private readonly NhQueryable<T> selection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public ReadOnlyRepository(ISession session)
        {
            this.selection = new NhQueryable<T>(session as ISessionImplementor);
        }
        #region IReadOnlyRepository<T> Members

        /// <summary>
        /// Linq-a-fies this instance.
        /// </summary>
        /// <returns>IQueryable interface for entities.</returns>
        public IQueryable<T> Linq()
        {
            return this.selection.AsQueryable();
        }

        #endregion
    }
}
