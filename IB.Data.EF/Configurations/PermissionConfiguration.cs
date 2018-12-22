using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IB.Data.EF.Configurations
{
    internal sealed class PermissionConfiguration : EntityConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.ToTable(nameof(Permission), Schema.App);

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(Permission.NameLength).IsUnicode(false);
            entity.Property(x => x.Description).HasMaxLength(Permission.DescriptionLength);
        }
    }
}
