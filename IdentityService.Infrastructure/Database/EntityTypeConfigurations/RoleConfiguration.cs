using IdentityService.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Database.EntityTypeConfigurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Name);

            builder.OwnsMany<RoleClaim>(own => own.RoleClaims, entityBuilder =>
            {
                entityBuilder.HasKey(k => k.Id);
                entityBuilder.Property(p => p.ClaimValue);
            });
        }
    }
}
