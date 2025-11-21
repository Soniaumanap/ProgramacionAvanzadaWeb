using System.Net.Http.Json;

namespace SGC.Web.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        public ApiClient(HttpClient http) => _http = http;

        public Task<T?> GetAsync<T>(string url) =>
            _http.GetFromJsonAsync<T>(url);

        public async Task<(bool ok, T? data, string? error)> PostAsync<T>(string url, object body)
        {
            var res = await _http.PostAsJsonAsync(url, body);
            if (res.IsSuccessStatusCode)
                return (true, await res.Content.ReadFromJsonAsync<T>(), null);
            return (false, default, await res.Content.ReadAsStringAsync());
        }

        public async Task<(bool ok, T? data, string? error)> PutAsync<T>(string url, object body)
        {
            var res = await _http.PutAsJsonAsync(url, body);
            if (res.IsSuccessStatusCode)
                return (true, await res.Content.ReadFromJsonAsync<T>(), null);
            return (false, default, await res.Content.ReadAsStringAsync());
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var res = await _http.DeleteAsync(url);
            return res.IsSuccessStatusCode;
        }
    }
}
