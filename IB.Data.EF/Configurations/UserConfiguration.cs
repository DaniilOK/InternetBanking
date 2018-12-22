using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IB.Data.EF.Configurations
{
    internal sealed class UserConfiguration : EntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable(nameof(User), Schema.App);

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Login).IsRequired().HasMaxLength(User.LoginLength);
            entity.Property(x => x.FirstName).IsRequired().HasMaxLength(User.FirstNameLength);
            entity.Property(x => x.LastName).IsRequired().HasMaxLength(User.LastNameLength);
            entity.Property(x => x.Inactive);
            entity.Property(x => x.Email).HasMaxLength(User.EmailLength);
            entity.Property(x => x.IsEmailConfirmed);
        }
    }
}
