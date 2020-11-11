using System.Text;

namespace Infrastructure.Jwt
{
    public class JwtOptions
    {
        /// <summary>
        ///     For configuration
        /// </summary>
        public JwtOptions()
        {
        }

        /// <summary>
        ///     For creating
        /// </summary>
        /// <param name="audience"></param>
        /// <param name="issuer"></param>
        /// <param name="securityKey"></param>
        /// <param name="expireHours"></param>
        public JwtOptions(string audience, string issuer, string securityKey, int expireHours)
        {
            Audience = audience;
            Issuer = issuer;
            SecurityKey = securityKey;
            ExpireHours = expireHours;
        }

        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int ExpireHours { get; set; }

        public byte[] SecurityKeyBytes =>
            !string.IsNullOrEmpty(SecurityKey) ? Encoding.UTF8.GetBytes(SecurityKey) : default;
    }
}