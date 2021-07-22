using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.DataObjects
{
    public class Channel
    {
        [JsonProperty]
        private string id { get; set; }
        [JsonProperty]
        private int type { get; set; }
        [JsonProperty]
        private string name { get; set; }

        [JsonIgnore]
        public Snowflake Id => Snowflake.Parse(id);

        [JsonIgnore]
        public ChannelType Type => (ChannelType)type;

        [JsonIgnore]
        public string Name => name;

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}
