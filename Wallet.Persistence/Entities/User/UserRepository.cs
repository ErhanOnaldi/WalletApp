using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence.Users;

namespace Wallet.Persistence.Entities.User;

public class UserRepository(AppDbContext dbContext) : Repository<Domain.Entities.User>(dbContext), IUserRepository
{
    public Task<Domain.Entities.User?> GetByEmailAsync(string email)
    {
        return DbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
    }
}
