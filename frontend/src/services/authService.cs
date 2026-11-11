
using CollabSphere.Infrastructure.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CollabSphere.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(
            string fullName, string email, string password);

        Task<User?> LoginAsync(
            string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(
            string fullName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(
                    "Vui lòng nhập đầy đủ thông tin.");
            }

            if (password.Length < 8)
            {
                throw new ArgumentException(
                    "Mật khẩu phải có ít nhất 8 ký tự.");
            }

            email = email.Trim().ToLowerInvariant();

            var existingUser = await _context.Users
                .AnyAsync(u => u.Email == email);

            if (existingUser)
            {
                throw new InvalidOperationException(
                    "Email đã được sử dụng.");
            }

            var salt = RandomNumberGenerator.GetBytes(16);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                100000,
                HashAlgorithmName.SHA256,
                32);

            var user = new User
            {
                FullName = fullName.Trim(),
                Email = email,
                Password = Convert.ToBase64String(salt)
                    + "$" + Convert.ToBase64String(hash)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(
            string email, string password)
        {
            email = email.Trim().ToLowerInvariant();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            var parts = user.Password.Split('$');

            if (parts.Length != 2)
                return null;

            var salt = Convert.FromBase64String(parts[0]);
            var savedHash = Convert.FromBase64String(parts[1]);

            var inputHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                100000,
                HashAlgorithmName.SHA256,
                32);

            if (!CryptographicOperations.FixedTimeEquals(
                savedHash, inputHash))
            {
                return null;
            }

            return user;
        }
    }
}

