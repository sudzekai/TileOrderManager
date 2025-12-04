using TelegramBot;
using TelegramBot.AppErrorHandler;

namespace BotStartupProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BotLogger.IsInfoLogEnabled = true;
            BotLogger.IsBasicLogEnabled = true;

            BotLogger.InfoOutput = info =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(info);
                Console.ResetColor();
            };

            BotLogger.ErrorOutput = error =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor();
            };

            BotLogger.LogOutput = log =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(log);
                Console.ResetColor();
            };

            TileOrderBot bot = new(Environment.GetEnvironmentVariable("TILE_ORDER_MANAGER_BOT_TOKEN"));

        }
    }
}
