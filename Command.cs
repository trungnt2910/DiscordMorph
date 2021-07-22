using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordMorph
{
    public abstract class Command
    {
        public abstract string[] Aliases { get; }
        public abstract string Description { get; }
        public abstract string Guide { get; }
        public abstract Task ExecuteCommand(string args);

        public static Dictionary<string, Command> Commands { get; private set; }

        public static async Task ExecuteAsync(string str)
        {
            var match = Regex.Match(str, "/(\\S*)\\s*([\\s\\S]*)");

            if (match.Groups.Count < 2)
            {
                throw new InvalidOperationException("Invalid command");
            }

            var command = Commands.GetValueOrDefault(match.Groups[1].Value.ToLowerInvariant());
            if (command == default(Command))
            {
                throw new InvalidOperationException("Command does not exist.");
            }

            if (match.Groups.Count >= 3)
            {
                await command.ExecuteCommand(match.Groups[2].Value);
            }
            else
            {
                await command.ExecuteCommand(string.Empty);
            }
        }

        static Command()
        {
            Commands = Assembly
                            .GetExecutingAssembly()
                            .GetTypes()
                            .Where(type => type.IsAssignableTo(typeof(Command)))
                            .Except(new[] {typeof(Command)})
                            .SelectMany(type =>
                            {
                                var instance = (Command)Activator.CreateInstance(type);
                                return instance.Aliases.Select(a => new
                                {
                                    Name = a.Trim().ToLowerInvariant(),
                                    Instance = instance
                                });
                            })
                            .ToDictionary(kvp => kvp.Name, kvp => kvp.Instance);
        }
    }
}
