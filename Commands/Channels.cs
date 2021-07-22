using DiscordMorph.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordMorph.Commands
{
    public class Channels : Command
    {
        public override string[] Aliases => new[] { "channels", "switchchannel", "sc" };

        public override string Description => "Quickly switch between channels";

        public override string Guide => @"
Usage: /channels @[ChannelPosition]
       /channels [ChannelId]
       /channels [GuildId]/[ChannelId]
       /channels";

        public override async Task ExecuteCommand(string args)
        {
            args = args.Trim();

            Channel channel = null;

            var match = Regex.Match(args, "^\\D*(\\d+)[ /]*(\\d+)\\D*$");

            if (match.Success && match.Groups.Count == 3)
            {
                var guildId = match.Groups[1].Value;
                var guildsCommand = new Guilds();
                await guildsCommand.ExecuteCommand(guildId);
                var channelId = match.Groups[2].Value;

                channel = Data.Channels.FirstOrDefault(c => c.Id.ToString() == channelId);
            }
            else
            {
                if (Data.GuildId == null)
                {
                    Console.WriteLine("Join a guild first!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(args))
                {
                    Console.WriteLine($"Channels in {Data.GuildName}:");
                    for (int i = 0; i < (Data.Channels?.Count ?? 0); ++i)
                    {
                        Console.WriteLine($"{i}.\t{Data.Channels[i]}");
                    }
                    return;
                }
                else if (args.StartsWith("@"))
                {
                    channel = Data.Channels[int.Parse(args[1..])];
                }
                else
                {
                    channel = Data.Channels.FirstOrDefault(c => c.Id.ToString() == args);
                }
            }


            if (channel == null)
            {
                throw new InvalidOperationException("Channel does not exist.");
            }

            Data.ChannelId = channel.Id.ToString();

            Console.WriteLine($"Switched to {channel}");
        }
    }
}
