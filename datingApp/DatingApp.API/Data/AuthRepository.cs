using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{

    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        
        public Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
            }
        }

        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExist(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}