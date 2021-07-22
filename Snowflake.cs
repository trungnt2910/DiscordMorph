using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordMorph
{
    public class Snowflake
    {
        private ulong _value = 0ul;
        private static ulong id = 0ul;

        public const ulong DiscordEpoch = 1420070400000;

        public override string ToString()
        {
            return _value.ToString();
        }

        public static Snowflake Parse(string str)
        {
            var snowflake = new Snowflake();
            snowflake._value = ulong.Parse(str);
            return snowflake;
        }

        public static Snowflake NewSnowflake()
        {
            var snowFlake = new Snowflake();
            var time = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            time -= DiscordEpoch;
            snowFlake._value |= time << 22;
            var currentId = Interlocked.Increment(ref id);
            snowFlake._value |= id & 0xFFF;

            return snowFlake;
        }
    }
}
