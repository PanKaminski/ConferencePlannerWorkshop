using System;
using System.Threading.Tasks;
using ConferencePlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConferencePlanner.Services
{
    public class AdminService : IAdminService
    {
        private readonly IServiceProvider serviceProvider;

        private bool doesAdminExist;

        public AdminService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<bool> AllowAdminUserCreationAsync()
        {
            if (doesAdminExist)
            {
                return false;
            }

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

            if (await dbContext.Users.AnyAsync(user => user.IsAdmin))
            {
                doesAdminExist = true;
                return false;
            }

            return true;
        }
    }
}
