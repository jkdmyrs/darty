namespace Darty.Core.ApiClient
{
    using Darty.Core.Resources.Responses;
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class DartyApiClient
    {
        private string _baseUrl;
        private readonly HttpClient _httpClient;

        public DartyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task CreateGame(string gameId, string player1, string player2)
        {
            string requestUrl = $"api/game?game={gameId}&player1={player1}&player2={player2}";
            HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, new StringContent(string.Empty)).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task DartThrow(string gameId, string player, int value, int multiplier)
        {
            string requestUrl = $"api/throw?game={gameId}&player={player}&val={value}&mult={multiplier}";
            HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, new StringContent(string.Empty)).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GenerateGameId()
        {
            string requestUrl = "api/id";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<GameModelResponse> GetGameById(string id)
        {
            string requestUrl = $"api/game?game={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl).ConfigureAwait(false);
            string gameJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<GameModelResponse>(gameJson, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
