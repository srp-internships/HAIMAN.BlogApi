

namespace BlogApi.Interfaces
{
    public interface IUserService
    {
        Task<List<UserGetDto>> GetAllUsers();
        Task<List<UserGetDto>> SearchUser(string value);

    }
}
