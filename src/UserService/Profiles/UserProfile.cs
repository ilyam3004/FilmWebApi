using UserService.Dtos.Responses;
using UserService.Dtos.Requests;
using UserService.Models;
using AutoMapper;

namespace UserService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<(RegisterRequest, string), User>()
            .ForMember(dest => dest.Login,  
                opt => opt.MapFrom(src => src.Item1.Login))
            .ForMember(dest => dest.Password,  
                opt => opt.MapFrom(src => src.Item2))
            .AfterMap(((src, dest) => 
                dest.UserId = Guid.NewGuid()));
        
        CreateMap<(User, string), UserResponse>()
            .ForMember(dest => dest.Login,  
                opt => opt.MapFrom(src => src.Item1.Login))  
            .ForMember(dest => dest.Token,  
                opt => opt.MapFrom(src => src.Item2));  
    }
}

