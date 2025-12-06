using BLL.DTO.Objects.Review.Create;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] int? page)
        {
            try
            {
                var reviews = await _service.GetSimpleReviewsAsync();

                if (page.HasValue)
                {
                    var pagedReviews = reviews.Skip((page.Value - 1) * 5)
                                          .Take(5)
                                          .ToList();
                    return Ok(pagedReviews);
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserReviews(long userId)
        {
            try
            {
                var reviews = await _service.GetUserReviewsAsync(userId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            try
            {
                var review = await _service.GetReviewFullByIdAsync(id);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/info")]
        public async Task<IActionResult> GetReviewInfo(int id)
        {
            try
            {
                var review = await _service.GetReviewInfoByIdAsync(id);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("user/{userId}/create")]
        public async Task<IActionResult> PostReview(long userId, ReviewCreateDto review)
        {
            try
            {
                var createdReview = await _service.CreateReviewAsync(userId, review);
                return Ok(createdReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var result = await _service.DeleteReviewAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}