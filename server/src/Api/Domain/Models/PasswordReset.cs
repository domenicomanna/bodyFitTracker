using System;

namespace Api.Domain.Models
{
    public class PasswordReset
    {
        public string Token { get; private set; }
        public int AppUserId { get; private set; }
        public DateTime Expiration { get; private set; }
        public virtual AppUser AppUser { get; private set; }

        protected PasswordReset() { }

        public PasswordReset(string token, int appUserId, DateTime expiration)
        {
            Token = token;
            AppUserId = appUserId;
            Expiration = expiration;
        }
    }
}
