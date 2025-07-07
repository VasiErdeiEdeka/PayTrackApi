using PayTrack.Application.Dtos;

namespace PayTrack.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();

    Task<UserDto> GetByIdAsync(int id);

    Task<UserDto> CreateAsync(CreateUserDto user);

    Task<UserDto> UpdateAsync(UserDto user);

    Task DeleteAsync(int id);
}