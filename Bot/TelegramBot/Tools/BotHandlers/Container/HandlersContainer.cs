using Telegram.Bot;
using TelegramBot.Tools.APIServices;
using TelegramBot.Tools.AppErrorHandler;
using TelegramBot.Tools.BotHandlers.Handlers.Errors;
using TelegramBot.Tools.BotHandlers.Handlers.Messages;
using TelegramBot.Tools.BotHandlers.Handlers.Updates;

namespace TelegramBot.Tools.BotHandlers.Container
{
    public class HandlersContainer
    {
        public readonly ErrorHandler Errors;
        public readonly MessageHandler Messages;
        public readonly UpdateHandler Updates;

        public HandlersContainer(TelegramBotClient bot, ApiServicesContainer services)
        {
            Errors = new(bot, services);
            Messages = new(bot, services);
            Updates = new(bot, services);

            BotLogger.SendInfo("Обработчики сообщений, ошибок и запросов инициализированы");
        }
    }
}
