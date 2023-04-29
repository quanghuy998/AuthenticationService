using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.SeedWork;

namespace IdentityService.Domain.Aggregates.Users
{
    public class User : Aggregate
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; }
        public string PasswordHash { get; }
        public string Address { get; private set; }
        public DateTime CreatedTime { get; }
        public DateTime ModifiedTime { get; private set; }

        public List<UserClaim> UserClaims { get; private set; }
        public List<UserRole> UserRoles { get; private set; }

        public User(string firstName, string lastName, string email, string userName, string passwordHash, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            PasswordHash = passwordHash;
            Address = address;
            CreatedTime = DateTime.UtcNow;
            ModifiedTime = DateTime.UtcNow;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void UpdateUser(string firstName, string lastName, string email, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            ModifiedTime = DateTime.UtcNow;
        }

        public List<string> GetUserClaims()
        {
            return UserClaims.Select(x => x.ClaimValue).ToList();
        }

        public List<string> GetFullClaims()
        {
            var claims = new List<string>();
            claims.AddRange(GetUserClaims());

            foreach(var userRole in UserRoles)
            {
                var roleClaims = userRole.Role.GetRoleClaims();
                claims.AddRange(roleClaims);
            };

            return claims;
        }

        public void AddClaim(string claimName)
        {
            var claim = new UserClaim(claimName);
            UserClaims.Add(claim);
        }

        public void AddUserRoles(List<UserRole> roles)
        {
            UserRoles = roles;
        }

        public void RemoveClaim(int claimId) 
        {
            var claim = UserClaims.FirstOrDefault(x => x.Id == claimId);
            UserClaims.Remove(claim);
            if (UserClaims is null)
                UserClaims = new();
        }
    }
}
