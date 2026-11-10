using CollabSphere.Infrastructure.Entities;
using CollabSphere.Infrastructure.Repositories;

namespace CollabSphere.Infrastructure.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(ulong id);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(ulong id);
}

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<User?> GetUserByIdAsync(ulong id)
    {
        return _repository.GetByIdAsync(id);
    }

    public async Task CreateUserAsync(User user)
    {
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _repository.Update(user);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(ulong id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with ID {id} was not found.");
        }

        _repository.Delete(user);
        await _repository.SaveChangesAsync();
    }
}
