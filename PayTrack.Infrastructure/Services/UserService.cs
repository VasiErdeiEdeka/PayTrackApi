using AutoMapper;
using PayTrack.Application;
using PayTrack.Application.Dtos;
using PayTrack.Application.Interfaces;
using PayTrack.Domain.Entities;
using PayTrack.Infrastructure.Repositories;

namespace PayTrack.Infrastructure.Services;

public class UserService(IRepository<User> userRepository, IMapper mapper) : IUserService
{
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync(x => x.IsActive);

        return mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);

        return mapper.Map<UserDto>(user.IsActive ? user : null);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto user)
    {
        if (user == null)
        {
            throw new CustomException(nameof(user));
        }

        var mappedUser = mapper.Map<User>(user);
        await userRepository.AddAsync(mappedUser);
        await userRepository.SaveChangesAsync();

        return mapper.Map<UserDto>(mappedUser);
    }

    public async Task<UserDto> UpdateAsync(UserDto user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var existingUser = await userRepository.GetByIdAsync(user.Id);
        if (existingUser is null || !existingUser.IsActive)
        {
            throw new CustomException("User not found.");
        }


        mapper.Map(user, existingUser);

        userRepository.Update(existingUser);
        await userRepository.SaveChangesAsync();

        return mapper.Map<UserDto>(existingUser);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null || !user.IsActive)
        {
            throw new CustomException("User not found.");
        }

        user.IsActive = false;
        userRepository.Update(user);

        await userRepository.SaveChangesAsync();
    }
}