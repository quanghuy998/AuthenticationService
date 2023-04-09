namespace IdentityService.Application.Dtos
{
    public class GetTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiredIn { get; set; }
    }
}
