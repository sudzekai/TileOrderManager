using BLL.DTO.Interfaces.Create;
using BLL.DTO.Interfaces.Special;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.Review.Main;
using BLL.DTO.Objects.Review.Special;
using BLL.DTO.Tools;
using BLL.Services.Interfaces;
using DAL.EfCore.Models;
using DAL.EfCore.UOW.Interface;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;

        public ReviewService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<ReviewListDto>> GetSimpleReviewsAsync()
        {
            var reviews = await _uow.Reviews.GetAllAsync();
            return [.. reviews.Select(r => r.ToDto<Review, ReviewListDto>())];
        }

        public async Task<List<ReviewListDto>> GetUserReviewsAsync(long id)
        {
            var reviews = await _uow.Reviews.GetAllByUserIdAsync(id);
            return [.. reviews.Select(r => r.ToDto<Review, ReviewListDto>())];
        }

        public async Task<ReviewFullDto> GetReviewFullByIdAsync(int id)
            => await GetByIdAsync<ReviewFullDto>(id);

        public async Task<ReviewInfoDto> GetReviewInfoByIdAsync(int id)
            => await GetByIdAsync<ReviewInfoDto>(id);

        private async Task<T> GetByIdAsync<T>(int id)
            where T : ISpecialDto<Review>, new()
        {
            var review = await _uow.Reviews.GetByIdAsync(id);

            return review == null ? throw new Exception($"Отзыв с Id {id} не найден")
                                  : review.ToDto<Review, T>();
        }

        public async Task<ReviewFullDto> CreateReviewAsync(long userId, ICreateDto<Review> review)
        {
            var createdReview = review.ToModel();

            createdReview.UserId = userId;

            await _uow.Reviews.AddAsync(createdReview);
            await _uow.SaveChagesAsync();

            return createdReview.ToDto<Review, ReviewFullDto>();
        }

        public async Task<bool> UpdateReviewAsync(int id, IUpdateDto<Review> review)
        {
            var foundReview = await _uow.Reviews.GetByIdAsync(id) ?? throw new Exception($"Отзыв с Id {id} не найден");

            foundReview.UpdateModel(review);

            await _uow.SaveChagesAsync();

            return true;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var result = await _uow.Reviews.DeleteAsync(id);
            if (!result)
                return false;

            await _uow.SaveChagesAsync();
            return true;
        }

    }
}
