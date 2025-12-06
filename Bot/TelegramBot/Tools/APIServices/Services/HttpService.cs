using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using TelegramBot.Tools.APIServices.Tools;
using TelegramBot.Tools.AppErrorHandler;

namespace TelegramBot.Tools.APIServices.Services
{
    public class HttpService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<T?> GetAsync<T>(string url) where T : class
        {
            try
            {
                BotLogger.SendLog($"Запрос на получение объекта {GetQueryObject(url)}\nURL: {url}");
                var responce = await _httpClient.GetAsync(url);

                await responce.CheckIfSucces(url);

                BotLogger.SendLog($"Получен объект {GetQueryObject(url)}\nURL: {url}");
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
                BotLogger.SendLog($"Запрос на создание объекта {GetQueryObject(url)}\nURL: {url}");

                var responce = await _httpClient.PostAsJsonAsync(url, body);

                await responce.CheckIfSucces(url);

                BotLogger.SendLog($"Успешно создан новый объект {GetQueryObject(url)}\nURL: {url}");
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
                BotLogger.SendLog($"Запрос на обновление объекта {GetQueryObject(url)}\nURL: {url}");

                var responce = await _httpClient.PutAsJsonAsync(url, body);

                await responce.CheckIfSucces(url);

                BotLogger.SendLog($"Успешно обновлен объект {GetQueryObject(url)}\nURL: {url}");
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
                BotLogger.SendLog($"Запрос на удаление объекта {GetQueryObject(url)}\nURL: {url}");

                var responce = await _httpClient.DeleteAsync(url);

                await responce.CheckIfSucces(url);

                BotLogger.SendLog($"Успешно удалён объект {GetQueryObject(url)}\nURL: {url}");
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
