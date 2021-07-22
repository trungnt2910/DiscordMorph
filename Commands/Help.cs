using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.Commands
{
    public class Help : Command
    {
        public override string Description
            => "Prints out the list of commands, as well as help for each of them.";

        public override string Guide
            => "Usage: /help [optional:CommandName]";

        public override string[] Aliases => new[] { "help", "guide" };

        public override Task ExecuteCommand(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.WriteLine("Welcome to DiscordMorph, a heaven for alt account users!");

                Console.WriteLine("Here are some of the commands available:");
                foreach (var kvp in Commands)
                {
                    var alias = kvp.Key;

                    // Skips a guide if it's not the primary alias
                    if (alias != kvp.Value.Aliases[0])
                    {
                        continue;
                    }

                    var name = kvp.Key.PadRight(20, ' ');
                    var description = kvp.Value.Description;

                    Console.WriteLine($"{name}{description}");
                }

                Console.WriteLine("For more details, type /help [CommandName]");
            }
            else
            {
                args = args.Trim().ToLowerInvariant();
                if (Commands.ContainsKey(args))
                {
                    Console.WriteLine(Commands[args].Description);
                    Console.WriteLine(Commands[args].Guide);
                }
                else
                {
                    Console.WriteLine("Invalid command!");
                }
            }

            return Task.CompletedTask;
        }
    }
}
