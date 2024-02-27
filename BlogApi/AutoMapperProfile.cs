

namespace BlogApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<Post, GetPostsDto>();
            CreateMap<GetPostsDto, Post>();
            CreateMap<CreatePostsDto, Post>();
            CreateMap<UpdatePostsDto, Post>();
        }
    }
}
