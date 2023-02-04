namespace AuthenticationService.Application.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedTime { get; }
        public DateTime ModifiedTime { get; }
    }
}
