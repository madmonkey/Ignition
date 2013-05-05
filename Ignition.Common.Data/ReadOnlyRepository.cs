
namespace Ignition.Common.Data
{
    using System.Linq;
    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The implementation of the IReadOnlyRepository
    /// </summary>
    /// <typeparam name="T">Entity used by NHibernate -> remember all properties MUST be declared as virtual - so a proxy can be generated.</typeparam>
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// The current session
        /// </summary>
        private readonly ISession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public ReadOnlyRepository(ISession session)
        {
            this.session = session;
        }
        
        #region IReadOnlyRepository<T> Members

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
        public System.Linq.IQueryProvider Provider
        {
            get { return session.Query<T>().Provider; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return session.Query<T>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.
        /// </summary>
        /// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public System.Type ElementType
        {
            get { return session.Query<T>().ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
        public System.Linq.Expressions.Expression Expression
        {
            get { return session.Query<T>().Expression; }
        }

        /// <summary>
        /// Marks as queryable and marshals to session.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> AsQueryable()
        {
            return session.Query<T>();
        }
        #endregion
    }
}
