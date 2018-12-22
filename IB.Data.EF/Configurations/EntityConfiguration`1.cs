using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IB.Data.EF.Configurations
{
    internal abstract class EntityConfiguration<TEntity> : EntityConfiguration where TEntity : Entity
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
