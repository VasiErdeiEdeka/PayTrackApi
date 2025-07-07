using Microsoft.AspNetCore.Mvc;
using PayTrack.Application.Dtos;
using PayTrack.Application.Interfaces;

namespace PayTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService service) : ControllerBase
{
    /// <summary>
    /// Gets all active users.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await service.GetAllUsersAsync());

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await service.GetByIdAsync(id));

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="dto">The user creation data.</param>
    [HttpPost]
    public async Task<IActionResult> Post(CreateUserDto dto) => Ok(await service.CreateAsync(dto));

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="dto">The user data to update.</param>
    [HttpPut]
    public async Task<IActionResult> Put(UserDto dto) => Ok(await service.UpdateAsync(dto));

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
