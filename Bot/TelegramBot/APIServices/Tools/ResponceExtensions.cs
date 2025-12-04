using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TelegramBot.APIServices.Tools
{
    public static class ResponceExtensions
    {
        public static async Task CheckIfSucces(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response.IsSuccessStatusCode} - {await ExtractErrorAsync(response)}");
        }

        private static async Task<string> ExtractErrorAsync(HttpResponseMessage response)
        {
            string raw = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(raw))
                return $"HTTP {(int)response.StatusCode} ({response.StatusCode}) — пустой ответ";

            try
            {
                using var doc = JsonDocument.Parse(raw);

                if (doc.RootElement.TryGetProperty("error", out var error))
                    return error.GetString() ?? raw;

                if (doc.RootElement.TryGetProperty("message", out var msg))
                    return msg.GetString() ?? raw;

                if (doc.RootElement.TryGetProperty("title", out var title))
                    return title.GetString() ?? raw;

                return raw;
            }
            catch
            {
                return raw;
            }
        }
    }
}
