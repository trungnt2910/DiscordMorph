using DiscordMorph.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.Commands
{
    public class Guilds : Command
    {
        public override string[] Aliases => new[] { "guilds", "switchguild", "sg" };

        public override string Description => "Quickly switch between guilds";

        public override string Guide =>
                        @"
Usage: /guilds @[GuildPosition]
       /guilds [GuildId]
       /guilds";

        public override async Task ExecuteCommand(string args)
        {
            args = args.Trim();

            Guild guild = null;

            if (Data.UserToken == null)
            {
                throw new InvalidOperationException("Log in before viewing guilds!");
            }

            if (string.IsNullOrWhiteSpace(args))
            {
                Console.WriteLine("Guilds:");
                for (int i = 0; i < (Data.Guilds?.Count ?? 0); ++i)
                {
                    Console.WriteLine($"{i}.\t{Data.Guilds[i]}");
                }
                return;
            }
            else if (args.StartsWith("@"))
            {
                guild = Data.Guilds[int.Parse(args[1..])];
            }
            else
            {
                guild = Data.Guilds.FirstOrDefault(g => g.Id.ToString() == args);
            }

            if (guild == null)
            {
                throw new InvalidOperationException("Guild does not exist.");
            }

            Data.GuildId = guild.Id.ToString();
            Data.GuildName = guild.Name;

            Data.Channels = await DiscordApi.ListChannels(guild, Data.UserToken);

            Console.WriteLine($"Switched to {guild}");
        }
    }
}
