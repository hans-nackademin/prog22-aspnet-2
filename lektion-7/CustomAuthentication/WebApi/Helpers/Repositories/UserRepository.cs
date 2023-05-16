using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class UserRepository : Repo<UserEntity>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
