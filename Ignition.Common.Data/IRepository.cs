
namespace Ignition.Common.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The writable interface
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Add(TEntity entity);

        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool SaveOrUpdate(TEntity entity);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool SaveOrUpdate(IEnumerable<TEntity> entities);
    }
}
