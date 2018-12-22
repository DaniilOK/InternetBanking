using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace IB.Data.EF.Configurations
{
    internal static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            EntityConfiguration<TEntity> configuration) where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>(configuration.Configure);
        }
    }
}
