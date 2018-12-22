using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IB.Data.EF.Configurations
{
    internal sealed class BankCardConfiguration : EntityConfiguration<BankCard>
    {
        public override void Configure(EntityTypeBuilder<BankCard> entity)
        {
            entity.ToTable(nameof(BankCard), Schema.App);

            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.BankAccount).WithMany().HasForeignKey(x => x.BankAccountId);
            entity.Property(x => x.Number);
            entity.Property(x => x.Validity);
            entity.Property(x => x.Active);
            entity.Property(x => x.VerificationCode);
            entity.Property(x => x.PinCode);
        }
    }
}
