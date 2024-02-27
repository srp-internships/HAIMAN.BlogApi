
namespace BlogApi.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> SearchUser(string value);

        Task SaveUsers(List<User> users);
        Task<User?> ExistsUser(int id);

        
    }
}
