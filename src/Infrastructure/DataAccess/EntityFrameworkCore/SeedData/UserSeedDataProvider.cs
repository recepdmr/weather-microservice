using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore.SeedData
{
    public class UserSeedDataProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly WeatherDbContext _weatherDbContext;

        public UserSeedDataProvider(WeatherDbContext weatherDbContext, UserManager<User> userManager)
        {
            _weatherDbContext = weatherDbContext;
            _userManager = userManager;
        }

        public async Task SeedDataAsync(CancellationToken cancellationToken)
        {
            if (!await _weatherDbContext.Users.AnyAsync(cancellationToken))
            {
                var user = new User("test1", "admin", "surname");
                await _userManager.AddPasswordAsync(user, "123456");

                await _weatherDbContext.Users.AddAsync(user, cancellationToken);

                await _weatherDbContext.SaveChangesAsync(cancellationToken);

                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.Name));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.Surname));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.UserData, user.Id.ToString("B")));
            }
        }
    }
}