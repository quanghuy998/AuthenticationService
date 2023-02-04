using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Aggregates.Users
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
        public DateTime ModifiedTime { get; }

        public List<UserClaim> UserClaims { get; }
        public List<UserRole> UserRoles { get; }

        public User(string firstName, string lastName, string email, string userName, string passwordHash, string address) 
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            PasswordHash = passwordHash;
            Address = address;
            CreatedTime = DateTime.UtcNow;
            ModifiedTime= DateTime.UtcNow;
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
        }
    }
}
