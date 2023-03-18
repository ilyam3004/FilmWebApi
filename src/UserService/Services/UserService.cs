using UserService.Dtos.Responses;
using UserService.Dtos.Requests;
using UserService.Models;
using UserService.Data;
using FluentValidation;
using AutoMapper;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterRequest> _validator;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper, IValidator<RegisterRequest> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = _mapper.Map<User>(request);
        await _userRepository.AddUser(user);

        return _mapper.Map<RegisterResponse>(user);
    }

    public Task<LoginResponse> Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}

