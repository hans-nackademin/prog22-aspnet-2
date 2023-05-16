using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApi.Contexts;
using WebApi.Models.Entities;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services
{
    public class UserManager
    {
        private readonly PasswordGenerator _passwordGenerator;
        private readonly DataContext _context;

        public UserManager(PasswordGenerator passwordGenerator, DataContext context)
        {
            _passwordGenerator = passwordGenerator;
            _context = context;
        }


        public async Task<bool> CreateUserAsync(UserEntity user, string password)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();

            _context.Add(new UserHash
            {
                UserId = user.Id,
                SecurityConcurrencyKey = _passwordGenerator.HashPassword(password)
            });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LoginAsync(LoginSchema schema)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == schema.Email);
            if (user != null) 
            {
                var userHash = await _context.UsersHashes.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (userHash != null)
                {
                    return _passwordGenerator.ValidatePassword(schema.Password, userHash.SecurityConcurrencyKey);
                }
                
            }

            return false;
        }
    }
}
