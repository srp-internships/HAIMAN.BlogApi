


namespace BlogApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User?> ExistsUser(int id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                return user;
            }
            return null;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dataContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task SaveUsers(List<User> users)
        {
            _dataContext.Users.AddRange(users);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<User>> SearchUser(string value)
        {
           return await _dataContext.Users.ToListAsync();
        }
    }
}
