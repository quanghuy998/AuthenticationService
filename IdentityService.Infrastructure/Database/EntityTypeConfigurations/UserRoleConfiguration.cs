using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Database.EntityTypeConfigurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasIndex(k => new { k.RoleId, k.UserId })
                .IsUnique();

            builder.HasOne<User>(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.HasOne<Role>(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
