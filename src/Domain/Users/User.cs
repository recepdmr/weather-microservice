using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            
        }

        public User(string userName,string name,string surname) : base(userName)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] RefreshTokenHash { get; set; }
        public DateTimeOffset RefreshTokenExpiresDate { get; set; }
    }
}