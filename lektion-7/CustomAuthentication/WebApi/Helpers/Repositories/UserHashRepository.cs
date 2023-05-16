using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class UserHashRepository : Repo<UserHashEntity>
    {
        public UserHashRepository(DataContext context) : base(context)
        {
        }
    }
}
