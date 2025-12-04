using BLL.DTO.Interfaces.Create;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.Review.Main;
using BLL.DTO.Objects.Review.Special;
using DAL.EfCore.Models;

namespace BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewListDto>> GetSimpleReviewsAsync();

        Task<List<ReviewListDto>> GetUserReviewsAsync(long id);

        Task<ReviewFullDto> GetReviewFullByIdAsync(int id);

        Task<ReviewInfoDto> GetReviewInfoByIdAsync(int id);

        Task<ReviewFullDto> CreateReviewAsync(long userId, ICreateDto<Review> review);

        Task<bool> UpdateReviewAsync(int id, IUpdateDto<Review> review);

        Task<bool> DeleteReviewAsync(int id);

    }
}
