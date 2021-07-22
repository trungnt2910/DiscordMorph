using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.Commands
{
    public class Quit : Command
    {
        public override string[] Aliases => new[] { "quit", "exit", "bye" };

        public override string Description => "Quits DiscordMorph";

        public override string Guide => "Usage: /quit";

        public override Task ExecuteCommand(string args)
        {
            Console.WriteLine("Bye!");
            Environment.Exit(0);
            throw new InvalidOperationException("What the fuck?");
        }
    }
}
