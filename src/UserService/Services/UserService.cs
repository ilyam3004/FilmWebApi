using UserService.Common.Exceptions;
using UserService.Dtos.Responses;
using UserService.Dtos.Requests;
using LanguageExt.Common;
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
        IMapper mapper, 
        IValidator<RegisterRequest> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<RegisterResponse>> Register(RegisterRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<RegisterResponse>(validationException);
        }
        
        if(_userRepository.UserExists(request.Login))
        {
            var exception = new DuplicateEmailException($"User with login {request.Login} already exists");
            return new Result<RegisterResponse>(exception);
        }

        var user = _mapper.Map<User>(request);
        await _userRepository.AddUser(user);

        return new Result<RegisterResponse>(
            _mapper.Map<RegisterResponse>(user));
    }

    public Task<LoginResponse> Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}