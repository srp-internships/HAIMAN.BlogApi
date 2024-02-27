namespace BlogApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserGetDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
          
            return _mapper.Map<List<UserGetDto>>(users);
        }

        public async Task<List<UserGetDto>> SearchUser(string value)
        {
            var users = await _userRepository.SearchUser(value);
            users = users.Where(u => u.FirstName.Contains(value, StringComparison.OrdinalIgnoreCase) 
                                  || u.LastName.Contains(value, StringComparison.OrdinalIgnoreCase)
                                  || u.FullName.Contains(value, StringComparison.OrdinalIgnoreCase))
                                  .ToList();
            
            return _mapper.Map<List<UserGetDto>>(users);
        }
    }
}
