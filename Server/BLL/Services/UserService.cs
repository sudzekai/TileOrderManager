using BLL.DTO.Interfaces.Create;
using BLL.DTO.Interfaces.Special;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.User.Main;
using BLL.DTO.Objects.User.Special;
using BLL.DTO.Tools;
using BLL.Services.Interfaces;
using DAL.EfCore.Models;
using DAL.EfCore.UOW.Interface;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<UserListDto>> GetUsersAsync()
        {
            var users = await _uow.Users.GetAllAsync();
            return [.. users.Select(u => u.ToDto<User, UserListDto>())];
        }

        public async Task<UserFullDto> GetByIdAsync(long id)
            => await GetUserByIdAsync<UserFullDto>(id);

        public async Task<UserInfoDto> GetInfoByIdAsync(long id)
            => await GetUserByIdAsync<UserInfoDto>(id);

        public async Task<UserChatInfoDto> GetUserChatInfoByIdAsync(long id)
            => await GetUserByIdAsync<UserChatInfoDto>(id);

        private async Task<T> GetUserByIdAsync<T>(long id)
            where T : ISpecialDto<User>, new()
        {
            var user = await _uow.Users.GetByIdAsync(id);

            return user == null ? throw new Exception($"Пользователь с Id: {id} не найден")
                                : user.ToDto<User, T>();
        }

        public async Task<UserFullDto> CreateUserAsync(ICreateDto<User> user)
        {
            var createdUser = user.ToModel();

            await _uow.Users.AddAsync(createdUser);
            await _uow.SaveChagesAsync();

            return createdUser.ToDto<User, UserFullDto>();
        }

        public async Task<bool> UpdateUserAsync(long id, IUpdateDto<User> user)
        {
            var foundUser = await _uow.Users.GetByIdAsync(id) ?? throw new Exception($"Пользователь с Id: {id} не найден");

            foundUser.UpdateModel(user);
            await _uow.SaveChagesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            var result = await _uow.Users.DeleteAsync(id);

            if (result)
                await _uow.SaveChagesAsync();

            return result;
        }
    }
}
