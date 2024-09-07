using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash, CancellationToken cancellationToken);
    }
}
