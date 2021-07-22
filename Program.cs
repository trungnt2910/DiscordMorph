using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordMorph
{
    using static Data;
    class Program
    {
        private static HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.InputEncoding = new UnicodeEncoding();
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("DiscordMorph - A Discord Client that allows you to switch between different accounts");

            while (true)
            {
                Console.Write($"{UserName}{(string.IsNullOrEmpty(GuildName) ? string.Empty : $"@{GuildName}")}>");
                var line = Console.ReadLine().Trim();
                try
                {
                    if (Regex.Match(line, "/(\\S*)\\s*([\\s\\S]*)").Success)
                    {
                        await Command.ExecuteAsync(line);
                    }
                    else
                    {
                        await SendMessageAsync(line);
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error: {e}");
                }
            }
        }

        public static async Task SendMessageAsync(string content)
        {
            ThrowIfNull(ChannelId, nameof(ChannelId));
            ThrowIfNull(UserId, nameof(UserId));
            ThrowIfNull(UserToken, nameof(UserToken));

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"https://discord.com/api/v9/channels/{ChannelId}/messages");
            request.Method = HttpMethod.Post;
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(UserToken);

            var nonce = Snowflake.NewSnowflake().ToString();
            var body = new
            {
                content = content,
                nonce = nonce,
                tts = false
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private static void ThrowIfNull(object o, string name)
        {
            if (o == null)
            {
                throw new ArgumentNullException($"{name} is null. Register it first!");
            }
        }
    }
}
