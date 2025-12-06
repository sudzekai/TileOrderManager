using System.Formats.Asn1;
using Telegram.Bot;
using TelegramBot.Tools.APIServices;
using TelegramBot.Tools.AppErrorHandler;
using TelegramBot.Tools.BotHandlers.Handlers.Messages.Tools;
using TelegramBot.Tools.Extensions;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.BotHandlers.Handlers.Messages
{
    public class MessageHandler
    {
        private readonly TelegramBotClient _bot;
        private readonly ApiServicesContainer _services;
        private readonly CommandsController _commands;

        public MessageHandler(TelegramBotClient bot, ApiServicesContainer services)
        {
            _bot = bot;
            _services = services;
            _commands = new(services);
        }

        public async Task OnMessage(Telegram.Bot.Types.Message message, Telegram.Bot.Types.Enums.UpdateType type)
        {
            if (string.IsNullOrWhiteSpace(message.Text)
                || message.From is null)
                return;

            long chatId = message.From.Id;
            string text = message.Text;

            BotLogger.SendLog($"Получено сообщение:\n" +
                $"\"{text}\"\n" +
                $"Пользователь: https://t.me/{message.From.Username ?? "-- Username отсутствует"}");

            MessageModel? answer = null;

            if (message.Text.StartsWith("/"))
                answer = await _commands.Control(message);

            if (answer is not null)
                await _bot.SendMessageModel(chatId, answer);
        }
    }
}
