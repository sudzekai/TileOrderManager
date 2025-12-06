using System.Reflection.Metadata.Ecma335;
using TelegramBot.Tools.AppErrorHandler;

namespace TelegramBot.Tools.Extensions
{
    public static class StringFormatter
    {
        public static string GetBetween(this string text, string start, string end)
        {
            try
            {
                int first = text.IndexOf(start);

                first = first + start.Length;

                int second = text.Substring(first).IndexOf(end);

                return text.Substring(first, second);
            }
            catch (Exception e)
            {
                BotLogger.HandleException(e);
                return "";
            }

        }

        public static long GetDigitsAfter(this string text, string start)
        {
            try
            {
                string result = "";

                int index = text.IndexOf(start);

                index = index + start.Length;
                text = text.Substring(index);

                foreach (char c in text.ToCharArray())
                {
                    if (char.IsDigit(c))
                        result += c;

                    else
                        break;
                }
                return Convert.ToInt64(result);
            }
            catch (Exception e)
            {
                BotLogger.HandleException(e);
                return 1;
            }
        }

        public static string GetOnlyFirstLetters(this string fullName)
        {
            var name = fullName.Split(' ');
            string result = "";
            foreach (var part in name)
            {
                result += $"{part.Trim()[0]}. ";
            }
            return result;
        }
    }
}
