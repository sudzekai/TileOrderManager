using BLL.DTO.Objects.User.Create;
using BLL.DTO.Objects.User.Main;
using BLL.DTO.Objects.User.Special;
using BLL.DTO.Objects.User.Update;

namespace TelegramBot.Tools.APIServices.Services
{
    public class UsersApiService
    {
        private readonly HttpService _httpService;
        private readonly string _baseUrl = "http://localhost:5293/api/users";

        public UsersApiService(HttpService service)
        {
            _httpService = service;
        }

        public async Task<List<UserListDto>> GetAllAsync(int page)
            => await _httpService.GetAsync<List<UserListDto>>($"{_baseUrl}?page={page}");

        public async Task<UserFullDto> GetByIdAsync(long userId)
            => await _httpService.GetAsync<UserFullDto>($"{_baseUrl}/{userId}");

        public async Task<UserInfoDto> GetInfoByIdAsync(long userId)
            => await _httpService.GetAsync<UserInfoDto>($"{_baseUrl}/{userId}/info");

        public async Task<UserChatInfoDto> GetChatInfoByIdAsync(long userId)
            => await _httpService.GetAsync<UserChatInfoDto>($"{_baseUrl}/{userId}/chat-info");

        public async Task<UserFullDto> CreateAsync(UserCreateDto user)
            => await _httpService.PostAsync<UserFullDto>($"{_baseUrl}/create", user);

        public async Task<bool> UpdateAsync(long userId, UserUpdateDto user)
            => await _httpService.PutAsync($"{_baseUrl}/{userId}/update", user);

        public async Task<bool> UpdateAsync(long userId, UserInfoDto user)
            => await _httpService.PutAsync($"{_baseUrl}/{userId}/info/update", user);

        public async Task<bool> UpdateAsync(long userId, UserChatInfoDto user)
            => await _httpService.PutAsync($"{_baseUrl}/{userId}/chat-info/update", user);

        public async Task<bool> DeleteAsync(long userId, UserUpdateDto user)
            => await _httpService.DeleteAsync($"{_baseUrl}/{userId}/delete");
    }
}
