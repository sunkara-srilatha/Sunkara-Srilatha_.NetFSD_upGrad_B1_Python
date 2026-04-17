using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(CreateUserDto createUserDto);
        Task<UserDto> LoginAsync(UserLoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> EmailExistsAsync(string email);
    }
}
