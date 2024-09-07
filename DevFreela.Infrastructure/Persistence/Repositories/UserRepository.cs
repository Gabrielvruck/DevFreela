using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Users
                .SingleOrDefaultAsync(u => 
                u.Email == email && 
                u.Password == passwordHash, 
                cancellationToken: cancellationToken
                );
        }
    }
}
