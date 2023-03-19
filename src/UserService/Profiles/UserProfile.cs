using UserService.Dtos.Responses;
using UserService.Dtos.Requests;
using UserService.Models;
using AutoMapper;

namespace UserService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequest, User>()
            .AfterMap(((src, dest) => 
                dest.UserId = Guid.NewGuid()));
        
        CreateMap<User, RegisterResponse>();
    }
}

