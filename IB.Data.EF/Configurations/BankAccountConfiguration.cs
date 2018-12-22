using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IB.Data.EF.Configurations
{
    internal sealed class BankAccountConfiguration : EntityConfiguration<BankAccount>
    {
        public override void Configure(EntityTypeBuilder<BankAccount> entity)
        {
            entity.ToTable(nameof(BankAccount), Schema.App);

            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            entity.Property(x => x.Number);
            entity.Property(x => x.EndDate);
            entity.Property(x => x.Active);
            entity.Property(x => x.Money);
        }
    }
}
