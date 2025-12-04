using BLL.DTO.Objects.Review.Create;
using BLL.DTO.Objects.Review.Main;
using BLL.DTO.Objects.Review.Special;

namespace TelegramBot.APIServices
{
    public class ReviewsService
    {
        private readonly HttpService _httpService;
        private readonly string _baseUrl = "http://localhost:5293/api/reviews";

        public ReviewsService(HttpService service)
        {
            _httpService = service;
        }

        public async Task<List<ReviewListDto>> GetAllAsync()
            => await _httpService.GetAsync<List<ReviewListDto>>(_baseUrl);

        public async Task<List<UserReviewListDto>> GetAllByUserIdAsync(long userId)
            => await _httpService.GetAsync<List<UserReviewListDto>>($"{_baseUrl}/user/{userId}");

        public async Task<ReviewFullDto> GetByIdAsync(int id)
            => await _httpService.GetAsync<ReviewFullDto>($"{_baseUrl}/{id}");

        public async Task<ReviewInfoDto> GetInfoByIdAsync(int id)
            => await _httpService.GetAsync<ReviewInfoDto>($"{_baseUrl}/{id}/info");

        public async Task<ReviewFullDto> CreateAsync(long userId, ReviewCreateDto review)
            => await _httpService.PostAsync<ReviewFullDto>($"{_baseUrl}/user/{userId}/create", review);

        public async Task<bool> DeleteAsync(int id)
            => await _httpService.DeleteAsync($"{_baseUrl}/{id}");

    }
}
