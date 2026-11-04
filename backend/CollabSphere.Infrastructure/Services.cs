using CollabSphere.Infrastructure.Entities;
using CollabSphere.Infrastructure.Repositories;

namespace CollabSphere.Infrastructure.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(int id);

    Task CreateUserAsync(User user);

    Task UpdateUserAsync(User user);

    Task DeleteUserAsync(int id);
}

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
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

    public async Task DeleteUserAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
        {
            throw new Exception("Không tìm thấy người dùng!");
        }

        _repository.Delete(user);

        await _repository.SaveChangesAsync();
    }
}
