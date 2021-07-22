using DiscordMorph.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.Commands
{
    public class Login : Command
    {
        public override string[] Aliases => new[] { "login" };

        public override string Description => "Logs a new Discord Account in.";

        public override string Guide => "Usage: /login [required:TOKEN]";

        public override async Task ExecuteCommand(string args)
        {
            args = args.Trim();
            if (string.IsNullOrEmpty(args))
            {
                throw new InvalidOperationException("Login requires a token.");
            }

            var user = await DiscordApi.GetUser(args);
            user.AccessToken = args;

            Console.WriteLine($"Successfully logged in as {user.Username}#{user.Discriminator}");
            Data.Users.Add(user);
        }
    }
}
