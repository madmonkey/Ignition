
namespace Ignition.Common.Data
{
    using System.Linq;

    /// <summary>
    /// Read-only interface
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IReadOnlyRepository<out TEntity> : IQueryable<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable();
    }
}
