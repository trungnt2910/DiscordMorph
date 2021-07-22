using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.DataObjects
{
    /// <summary>
    /// The user structure, as defined in https://discord.com/developers/docs/resources/user#user-object
    /// </summary>
    public class User
    {
        [JsonProperty]
        private string id { get; set; }
        [JsonProperty]
        private string username { get; set; }
        [JsonProperty]
        private string discriminator { get; set; }

        [JsonIgnore]
        public Snowflake Id => Snowflake.Parse(id);

        [JsonIgnore]
        public string Username => username;

        [JsonIgnore]
        public int Discriminator => int.Parse(discriminator);

        // This is not an official field.
        [JsonIgnore]
        public string AccessToken { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Username}#{Discriminator}";
        }
    }
}
