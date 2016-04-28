using System;

namespace XamarinEvolve.Clients.Portable
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AccountResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
    }

    [Flags]
    public enum Access
    {
        None = 0,
        Admin = 1,
        Write = 1 << 1,
        Read = 1 << 2,
    }
}

