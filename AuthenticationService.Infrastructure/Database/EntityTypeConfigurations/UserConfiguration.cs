using AuthenticationService.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Infrastructure.Database.EntityTypeConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.PasswordHash).IsRequired();
            builder.Property(p => p.Address);
            builder.Property(p => p.CreatedTime).IsRequired();
            builder.Property(p => p.ModifiedTime).IsRequired();

            builder.OwnsMany<UserClaim>(own => own.UserClaims, entityBuilder =>
            {
                entityBuilder.HasKey(k => k.Id);
                entityBuilder.Property(p => p.ClaimValue).IsRequired();
            });


        }
    }
}
