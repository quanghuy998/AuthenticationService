namespace AuthenticationService.Application.Helpers
{
    internal static class HashPasswordHelper
    {
        private const string passwordPrefix = "1000|";
        public static string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return passwordPrefix + hashedPassword;
        }

        public static bool Verify(string password, string hashedPassword)
        {
            var exactlyHashedPassword = hashedPassword.Replace(passwordPrefix, "");

            return BCrypt.Net.BCrypt.Verify(password, exactlyHashedPassword);
        }
    }
}
