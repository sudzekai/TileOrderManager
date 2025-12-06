using TelegramBot;
using TelegramBot.Tools.AppErrorHandler;

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

            while (true)
            {
                Console.WriteLine($"{new string('=', 100)}\nМеню:\nВыкл бота: 0\nВкл/Выкл логов ({(BotLogger.IsBasicLogEnabled ? "Включены" : "Выключены")}): 1\n{new string('=', 100)}");
                var query = Console.ReadLine();

                if (query.Equals("0"))
                {
                    Environment.Exit(0);
                }
                else if (query.Equals("1"))
                {
                    BotLogger.IsBasicLogEnabled = !BotLogger.IsBasicLogEnabled;
                }
            }
        }
    }
}
