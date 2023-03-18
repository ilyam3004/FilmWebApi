using UserService.Dtos.Requests;
using UserService.Models;
using AutoMapper;

namespace UserService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequest, User>();
    }
}

