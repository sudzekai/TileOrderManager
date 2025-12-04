using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using TelegramBot.APIServices.Tools;
using TelegramBot.AppErrorHandler;

namespace TelegramBot.APIServices
{
    public class HttpService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<T?> GetAsync<T>(string url) where T : class
        {
            try
            {
                BotLogger.SendLog($"Запрос на получение объекта {GetQueryObject(url)}");
                var responce = await _httpClient.GetAsync(url);

                await responce.CheckIfSucces();

                BotLogger.SendLog($"Получен объект {GetQueryObject(url)}");
                return await responce.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                BotLogger.HandleException(ex);
                return null;
            }
        }

        public async Task<T?> PostAsync<T>(string url, object body) where T : class
        {
            try
            {
                BotLogger.SendLog($"Запрос на создание объекта {GetQueryObject(url)}");

                var responce = await _httpClient.PostAsJsonAsync(url, body);

                await responce.CheckIfSucces();

                BotLogger.SendLog($"Успешно создан новый объект {GetQueryObject(url)}");
                return await responce.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                BotLogger.HandleException(ex);
                return null;
            }
        }

        public async Task<bool> PutAsync(string url, object body)
        {
            try
            {
                BotLogger.SendLog($"Запрос на обновление объекта {GetQueryObject(url)}");

                var responce = await _httpClient.PostAsJsonAsync(url, body);

                await responce.CheckIfSucces();

                BotLogger.SendLog($"Успешно обновлен объект {GetQueryObject(url)}");
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            catch (Exception ex)
            {
                BotLogger.HandleException(ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string url)
        {
            try
            {
                BotLogger.SendLog($"Запрос на удаление объекта {GetQueryObject(url)}");

                var responce = await _httpClient.DeleteAsync(url);

                await responce.CheckIfSucces();

                BotLogger.SendLog($"Успешно удалён объект {GetQueryObject(url)}");
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            catch (Exception ex)
            {
                BotLogger.HandleException(ex);
                return false;
            }
        }

        private string GetQueryObject(string url)
        {
            string obj = $"{url.Split("/")[4].TrimEnd("s")}";
            return char.ToUpper(obj[0]) + obj.Substring(1);
        }
    }
}
