using DiscordMorph.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph
{
    public static class DiscordApi
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<List<Guild>> ListGuilds(string accessToken)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://discord.com/api/v9/users/@me/guilds");
            request.Method = HttpMethod.Get;
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(accessToken);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Guild>>(json);
        }

        public static async Task<User> GetUser(string accessToken)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri("https://discord.com/api/v9/users/@me");
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(accessToken);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Failed to log in. Check your token and try again.");
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<User>(json);
        }

        public static async Task<List<Channel>> ListChannels(Guild guild, string accessToken)
        {
            return await ListChannels(guild.Id.ToString(), accessToken);
        }

        public static async Task<List<Channel>> ListChannels(string guildId, string accessToken)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"https://discord.com/api/v9/guilds/{guildId}/channels");
            request.Method = HttpMethod.Get;
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(accessToken);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Channel>>(json);
        }
    }
}
