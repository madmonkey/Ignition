
namespace Ignition.Common.Data
{
    using System.Linq;

    /// <summary>
    /// Read-only interface
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IReadOnlyRepository<out TEntity> where TEntity : class
    {
        /// <summary>
        /// Linqs this instance.
        /// </summary>
        /// <returns>IQueryable interface for TEntity</returns>
        IQueryable<TEntity> Linq();
    }
}
