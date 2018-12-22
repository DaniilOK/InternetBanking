using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IB.Data.EF.Configurations
{
    internal sealed class UserPermissionConfiguration : EntityConfiguration<UserPermission>
    {
        public override void Configure(EntityTypeBuilder<UserPermission> entity)
        {
            entity.ToTable(nameof(UserPermission), Schema.App);

            entity.HasKey(x => new { x.UserId, x.PermissionId });

            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId);
        }
    }
}
