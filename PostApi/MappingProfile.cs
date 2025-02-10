using AutoMapper;
using PostApi.DTO;
using PostApi.Models;
namespace PostApi
{
    public class MappingProfile: Profile
    {
        public MappingProfile() { 
        
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")));
        
            CreateMap<CreatePostDto, Post>();
        
        }
    }
}
