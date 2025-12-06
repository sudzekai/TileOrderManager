using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Types.Messages
{
    public class MessageModel
    {
        public string Text { get; set; } = "";

        public InlineKeyboardMarkup Markup { get; set; } = new();

        public MessageModel() { }

        public MessageModel(string text, InlineKeyboardMarkup markup) 
        {
            Text = text;
            Markup = markup;
        }
    }
}
