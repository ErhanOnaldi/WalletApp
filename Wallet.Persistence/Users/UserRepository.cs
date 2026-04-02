using Wallet.Application.Interfaces.Persistence.Users;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.Users;

public class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
}
