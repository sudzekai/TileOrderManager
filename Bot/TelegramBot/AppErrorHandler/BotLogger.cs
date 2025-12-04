using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace TelegramBot.AppErrorHandler
{
    public static class BotLogger
    {
        /// <summary>
        /// Делегат, куда отправлять обработанные ошибки.
        /// По умолчанию пишет в консоль.
        /// Можно переназначить из другого проекта.
        /// </summary>
        public static Action<string> ErrorOutput { get; set; }
        public static Action<string> LogOutput { get; set; }
        public static Action<string> InfoOutput { get; set; }

        private static bool _isErrorHandlerInitialized = false;
        public static bool IsBasicLogEnabled = false;
        public static bool IsInfoLogEnabled = false;

        /// <summary>
        /// Подключает обработчики для разных типов приложений.
        /// Вызывается один раз в старте программы.
        /// </summary>
        public static void InitializeErrorHandler()
        {
            if (_isErrorHandlerInitialized)
                return;

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            _isErrorHandlerInitialized = true;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            HandleException(ex);
            if (e.IsTerminating)
                Environment.Exit(-1);
        }

        private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            HandleException(e.Exception);
        }

        public static void HandleException(Exception ex)
        {
            var frame = new StackTrace(ex, true).GetFrame(0);

            string file = frame?.GetFileName() ?? "Unknown";
            
            FileInfo fileInfo = new(file);
            
            if (fileInfo.Exists)
                file = fileInfo.Name.Replace(".cs", "");

            int line = frame?.GetFileLineNumber() ?? -1;

            SendError($"[{file}] [{ex.GetType().Name.ToUpper()}] Сообщение: {ex.Message}\nСтрока: {line}\nПолный путь: {ex.StackTrace}");
        }

        public static void SendLog(string message, [CallerMemberName] string? methodName = null, [CallerFilePath] string? filePath = null)
        {
            if (IsBasicLogEnabled)
            {
                string fileName = "";
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    fileName = fileInfo.Name.Replace(".cs", "") + ".";
                }
                LogOutput?.Invoke($"{MessageConverter($"{GetTimeStamp()} [{fileName}{methodName}] [LOG] {message}")}");
            }
        }

        public static void SendInfo(string message, [CallerMemberName] string? methodName = null)
        {
            if (IsInfoLogEnabled)
                InfoOutput?.Invoke($"{MessageConverter($"{GetTimeStamp()} [{methodName}] [INFO] {message}")}");
        }

        public static void SendError(string message)
        {
            if (_isErrorHandlerInitialized)
                ErrorOutput?.Invoke($"{MessageConverter($"{GetTimeStamp()} {message}")}");
        }

        private static string GetTimeStamp()
            => $"[{DateTime.Now:HH:mm:ss}]";

        private static string MessageConverter(string message)
        {
            var lines = message.Split('\n');

            if (lines.Length == 0)
                return message;

            string first = lines[0].TrimEnd();

            int firstBracket = first.IndexOf(']');
            int secondBracket = first.IndexOf(']', firstBracket + 1);

            int indent = (secondBracket == -1 ? firstBracket : secondBracket) + 2;

            string pad = new(' ', indent);

            var sb = new StringBuilder();
            sb.AppendLine(first);

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].TrimEnd();

                foreach (var sub in line.Split('\r'))
                {
                    if (string.IsNullOrWhiteSpace(sub))
                        continue;

                    sb.AppendLine(pad + sub.Trim());
                }
            }
            return sb.ToString().Trim();
        }
    }
}
