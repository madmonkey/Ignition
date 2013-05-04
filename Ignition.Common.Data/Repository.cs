
namespace Ignition.Common.Data
{
    using NHibernate;
    using System.Collections.Generic;

    /// <summary>
    /// The base repository implementation.
    /// </summary>
    /// <typeparam name="T">Entity used for nHibernate</typeparam>
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class
    {
        /// <summary>
        /// The ISession
        /// </summary>
        private readonly ISession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public Repository(ISession session)
            : base(session)
        {
            this.session = session;
        }

        #region IRepository<T> Members

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Add(T entity)
        {
            this.session.Save(entity);
            return true;
        }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Add(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.session.Save(item);
            }

            return true;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Update(T entity)
        {
            this.session.Update(entity);
            return true;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Delete(T entity)
        {
            this.session.Delete(entity);
            return true;
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.session.Delete(entity);
            }

            return true;
        }

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.session.Update(entity);
            }

            return true;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool SaveOrUpdate(T entity)
        {
            this.session.SaveOrUpdate(entity);
            return true;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool SaveOrUpdate(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.session.SaveOrUpdate(entity);
            }

            return true;
        }

        #endregion
    }
}
