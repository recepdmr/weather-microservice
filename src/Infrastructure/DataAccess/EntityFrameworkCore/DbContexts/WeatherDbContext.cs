using System;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore.DbContexts
{
    public class WeatherDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }
    }
}