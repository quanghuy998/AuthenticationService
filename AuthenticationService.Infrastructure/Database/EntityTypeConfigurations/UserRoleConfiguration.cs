using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Infrastructure.Database.EntityTypeConfigurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(k => new { k.RoleId, k.UserId });

            builder.HasOne<User>(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.HasOne<Role>(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
