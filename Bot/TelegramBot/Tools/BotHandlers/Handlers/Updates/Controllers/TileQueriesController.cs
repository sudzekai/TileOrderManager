using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Tools.APIServices.Services;
using TelegramBot.Tools.Extensions;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.BotHandlers.Handlers.Updates.Controllers
{
    public class TileQueriesController
    {
        private Regex Tiles = new(@"tiles\?page=\d+");

        private Regex TileInfo = new(@"tiles\?id=\d+");

        private Regex TileCreate = new(@"tiles-create");

        private Regex TileCalculate = new(@"tiles-\d+-calculate");

        private Regex TileUpdate = new(@"tiles\?id=\d+-update");

        private TilesApiService _service;

        public TileQueriesController(TilesApiService service)
        {
            _service = service;
        }

        public async Task<MessageModel?> Control(Update update)
        {
            string data = update.CallbackQuery.Data;

            if (Tiles.IsMatch(data))
            {
                int page = Convert.ToInt32(data.GetDigitsAfter("page="));

                var tiles = await _service.GetAllAsync(page);

                var nextPageTiles = await _service.GetAllAsync(page + 1);

                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                foreach (var tile in tiles)
                    markup.AddButton($"{tile.Name}", $"tiles?id={tile.Id}").AddNewRow();

                if (page > 1)
                    markup.AddButton("⏪", $"tiles?page={page - 1}");
                else
                    markup.AddButton("-");

                markup.AddButton("Назад", "start");

                if (nextPageTiles.Count > 0)
                    markup.AddButton("⏩", $"tiles?page={page + 1}");
                else
                    markup.AddButton("-");

                return new("Виды брусчатки", markup);
            }

            else if (TileInfo.IsMatch(data))
            {
                int id = Convert.ToInt32(data.GetDigitsAfter("id="));

                var tile = await _service.GetByIdAsync(id);

                var markup = new InlineKeyboardMarkup().AddButton("Заказать", $"orders?tileid={id}-create")
                                                       .AddButton("Узнать стоимость", $"tiles-{id}-calculate").AddNewRow()
                                                       .AddButton("Назад", "tiles?page=1");

                return new($"*{tile.Name}*\nСтоимость: *{tile.Price}*\nОписание: _{tile.Description}_", markup);
            }

            else if (TileCreate.IsMatch(data))
            {
                return new($"Логика создания брусчатки", new());
            }

            else if (TileUpdate.IsMatch(data))
            {
                return new($"Логика обновления брусчатки", new());
            }

            else if (TileCalculate.IsMatch(data))
            {
                return new($"Логика подсчета стоимости брусчатки в заказе", new());
            }

            return null;
        }
    }
}
