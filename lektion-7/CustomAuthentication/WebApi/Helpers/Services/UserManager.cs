using WebApi.Helpers.Repositories;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Services
{
    public class UserManager
    {
        private readonly UserRepository _userRepo;
        private readonly UserHashRepository _userHashRepo;
        private readonly PasswordGenerator _passwordGenerator;
        private readonly TokenGenerator _tokenGenerator;

        public UserManager(UserRepository userRepo, UserHashRepository userHashRepo, PasswordGenerator passwordGenerator, TokenGenerator tokenGenerator)
        {
            _userRepo = userRepo;
            _userHashRepo = userHashRepo;
            _passwordGenerator = passwordGenerator;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<bool> CreateUserAsync(UserEntity user, string password)
        {
            if (!await _userRepo.AnyAsync(x => x.Email == user.Email))
            {
                user = await _userRepo.AddAsync(user);

                var userHash = await _userHashRepo.AddAsync(new UserHashEntity
                {
                    UserId = user.Id,
                    SecurityHash = _passwordGenerator.GeneratePasswordHash(password)
                });

                if (userHash != null)
                    return true;

            }

            return false;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetAsync(x => x.Email == email);
            if (user != null)
            {
                var userHash = await _userHashRepo.GetAsync(x => x.UserId == user.Id);
                if (userHash != null)
                {
                    if (_passwordGenerator.ValidatePassword(password, userHash.SecurityHash))
                        return _tokenGenerator.GenerateJwtToken(user);
                }
            }

            return null!;
        }
    }
}
