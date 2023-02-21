namespace AuthenticationService.Dtos
{
    public class UserRequest
    {
        public record CreateUserRequest(string firstName, string lastName, string email, string userName, string password, string address);
        public record UpdateUserRequest(string firstName, string lastName, string email, string address);
        public record UserLoginRequest(string userName, string password);

    }
}
