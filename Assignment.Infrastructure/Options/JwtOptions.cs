
namespace Assignment.Infrastructure.Options
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string ExpiryTime { get; set; }
    }
}
