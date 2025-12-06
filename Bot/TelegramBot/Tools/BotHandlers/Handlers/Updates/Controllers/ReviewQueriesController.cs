using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Tools.APIServices.Services;
using TelegramBot.Tools.Extensions;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.BotHandlers.Handlers.Updates.Controllers
{
    public class ReviewQueriesController
    {
        private Regex Reviews = new(@"reviews\?page=\d+");
        private Regex ReviewInfo = new(@"reviews\?id=\d+");
        private Regex ReviewCreate = new(@"reviews\?orderid=\d+-create");

        ReviewsApiService _service;
        public ReviewQueriesController(ReviewsApiService service)
        {
            _service = service;
        }

        public async Task<MessageModel?> Control(Update update)
        {
            string data = update.CallbackQuery.Data;

            if (Reviews.IsMatch(data))
            {
                int page = Convert.ToInt32(data.GetDigitsAfter("page="));

                var reviews = await _service.GetAllAsync(page);
                var nextPageReviews = await _service.GetAllAsync(page + 1);

                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                foreach (var review in reviews)
                    markup.AddButton($"{review.Title}", $"reviews-{review.Id}").AddNewRow();

                if (page > 1)
                    markup.AddButton("⏪", $"reviews?page={page - 1}");
                else
                    markup.AddButton("-");

                markup.AddButton("Назад", "start");

                if (nextPageReviews.Any())
                    markup.AddButton("⏩", $"reviews?page={page - 1}");
                else
                    markup.AddButton("-");

                MessageModel model = new MessageModel("Список отзывов", markup);

                return model;
            }
            else if (ReviewInfo.IsMatch(data))
            {
                int id = Convert.ToInt32(data.GetDigitsAfter("id="));

                var review = await _service.GetInfoByIdAsync(id);

                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                markup.AddButton("Назад", $"reviews?page=1");

                MessageModel model = new MessageModel($"*{review.Title}*\n_{review.Text}\nОценка: {review.Grade} из 10", markup);

                return model;
            }
            else if (ReviewCreate.IsMatch(data))
            {
                int id = Convert.ToInt32(data.GetDigitsAfter("orderid="));

                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                markup.AddButton("Назад", $"reviews?page=1");

                MessageModel model = new MessageModel($"*Логика создания отзыва*", markup);
                return model;
            }

            return null;
        }
    }
}
