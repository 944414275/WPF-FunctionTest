 
namespace AspNetCoreJWTTest2
{
    public class JWTConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string IssuerSigningKey { get; set; }
        public int AccessTokenExpiresMinutes { get; set; }
    }
}
