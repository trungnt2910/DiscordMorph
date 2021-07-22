using DiscordMorph.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.Commands
{
    class Switch : Command
    {
        public override string[] Aliases => new[] { "switch", "users", "su" };

        public override string Description => "Quickly switches between users";

        public override string Guide =>
            @"
Usage: /switch [UserName]
       /switch [UserName]#[Discriminator]
       /switch @[AccountPosition]
       /switch [AccessToken]";


        public override async Task ExecuteCommand(string args)
        {
            args = args.Trim();

            User newUser = null;

            if (args.StartsWith("@"))
            {
                var pos = int.Parse(args[1..]);
                newUser = Data.Users[pos];
            }
            else if (args.Contains("#"))
            {
                var parts = args.Split("#");
                var name = parts[0];
                var discriminator = int.Parse(parts[1]);

                newUser = Data.Users.FirstOrDefault(u => u.Username == name && u.Discriminator == discriminator);
                
                if (newUser == null)
                {
                    throw new InvalidOperationException($"User {args} does not exist.");
                }
            }
            else if (args.Length > 32)
            {
                newUser = Data.Users.FirstOrDefault(u => u.AccessToken == args);

                if (newUser == null)
                {
                    var loginCommand = new Login();
                    await loginCommand.ExecuteCommand(args);

                    newUser = Data.Users.Last();
                }
            }
            else if (!string.IsNullOrWhiteSpace(args))
            {
                newUser = Data.Users.FirstOrDefault(u => u.Username == args);
                if (newUser == null)
                {
                    throw new InvalidOperationException($"User {args} does not exist");
                }
            }
            else
            {
                Console.WriteLine("Registered users:");
                for (int i = 0; i < Data.Users.Count; ++i)
                {
                    Console.WriteLine($"{i}.\t{Data.Users[i]}");
                }
                return;
            }


            Data.UserId = newUser.Id.ToString();
            Data.UserName = $"{newUser.Username}#{newUser.Discriminator}";
            Data.UserToken = newUser.AccessToken;

            Data.Guilds = await DiscordApi.ListGuilds(newUser.AccessToken);

            // Signs out of the Guild and Channel if current user does not have access
            if (Data.Guilds.FirstOrDefault(g => g.Id.ToString() == Data.GuildId) == null)
            {
                Data.GuildId = null;
                Data.GuildName = null;
                Data.Channels = null;
                Data.ChannelId = null;
            }
            else
            {
                Data.Channels = await DiscordApi.ListChannels(Data.GuildId, Data.UserToken);
                if (Data.Channels.FirstOrDefault(c => c.Id.ToString() == Data.ChannelId) == null)
                {
                    Data.ChannelId = null;
                }
            }

            Console.WriteLine($"Switched to {Data.UserName}");
        }
    }
}
