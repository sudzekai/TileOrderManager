using BLL.DTO.Interfaces.Create;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.User.Main;
using BLL.DTO.Objects.User.Special;
using DAL.EfCore.Models;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserListDto>> GetUsersAsync();

        Task<UserFullDto> GetByIdAsync(long id);

        Task<UserInfoDto> GetInfoByIdAsync(long id);

        Task<UserChatInfoDto> GetUserChatInfoByIdAsync(long id);

        Task<UserFullDto> CreateUserAsync(ICreateDto<User> user);

        Task<bool> UpdateUserAsync(long id, IUpdateDto<User> user);

        Task<bool> DeleteUserAsync(long id);
    }
}
