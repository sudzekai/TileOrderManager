using System.ComponentModel.Design;
using Telegram.Bot;
using TelegramBot.Tools.APIServices;
using TelegramBot.Tools.AppErrorHandler;

namespace TelegramBot.Tools.BotHandlers.Handlers.Errors
{
    public class ErrorHandler
    {
        private readonly TelegramBotClient _bot;
        private readonly ApiServicesContainer _services;

        public ErrorHandler(TelegramBotClient bot, ApiServicesContainer services)
        {
            _bot = bot;
            _services = services;
        }

        public Task OnError(Exception exception, Telegram.Bot.Polling.HandleErrorSource source)
        {
            BotLogger.HandleException(exception);
            return Task.CompletedTask;
        }
    }
}
