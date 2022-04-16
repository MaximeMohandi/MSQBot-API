namespace MSQBot_API.Core.Entities
{
    public class JwtConfiguration
    {
        /// <summary>
        /// Toggle validation of issuer that's signing the key
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; }

        /// <summary>
        /// Key used to validate a signature
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// Toggle the issuer validation during token validation
        /// </summary>
        public bool ValidateIssuer { get; set; } = true;

        /// <summary>
        /// A valid issuer used to check agains token issuer
        /// </summary>
        public string ValidIssuer { get; set; }

        /// <summary>
        /// Toggle the audience validation during token validation
        /// </summary>
        public bool ValidateAudience { get; set; } = true;

        /// <summary>
        /// A valid audience used to check agains token issuer
        /// </summary>
        public string ValidAudience { get; set; }

        /// <summary>
        /// Toggle if the token will have an expiration date
        /// </summary>
        public bool RequireExpirationTime { get; set; }

        /// <summary>
        /// Toggle if the lifetime will be validated during token validation.
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;
    }
}