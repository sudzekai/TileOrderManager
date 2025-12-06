using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Tools.Extensions;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.Extensions
{
    public static class BotExtension
    {
        public static async Task<Message> SendMessageModel(this TelegramBotClient bot, long chatIdm, MessageModel message)
            => await bot.SendMessage(chatIdm, message.Text, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: message.Markup);

        public static async Task<Message> EditMessageModel(this TelegramBotClient bot, long chatId, int messageId, MessageModel message)
            => await bot.EditMessageText(chatId, messageId, message.Text, Telegram.Bot.Types.Enums.ParseMode.Markdown, message.Markup);
    }
}
