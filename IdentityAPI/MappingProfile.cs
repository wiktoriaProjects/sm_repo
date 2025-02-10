
using AutoMapper;
using IdentityApi.DTO;
using IdentityApi.Models;
namespace IdentityApi
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {

            CreateMap<UserRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        
        
        }
    }
}
