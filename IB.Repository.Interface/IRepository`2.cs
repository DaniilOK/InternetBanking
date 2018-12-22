using IB.Repository.Interface.Models;

namespace IB.Repository.Interface
{
    public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        TEntity Find(TKey id);
    }
}
