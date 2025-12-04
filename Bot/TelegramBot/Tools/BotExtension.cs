using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Tools
{
    public static class BotExtension
    {
        public static async Task<Message> SendMessage(this TelegramBotClient bot, long chatIdm)
            => await bot.SendMessage(chatIdm);

        public static async Task<Message> EditMessageText(this TelegramBotClient bot, int messageId, long chatId)
            => await bot.EditMessageText(messageId, chatId);
    }
}
