namespace BlogApi.Interfaces
{
    public interface IJsonPlaceholder
    {
        Task<List<Post>> FetchPosts();
        Task<List<User>> FetchUsers();



    }
}
