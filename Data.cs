using DiscordMorph.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph
{
    public static class Data
    {
        #region Current User
        public static string ServerId { get; set; }
        public static string ChannelId { get; set; }
        public static string UserId { get; set; }
        public static string UserToken { get; set; }
        public static string UserName { get; set; } = string.Empty;
        #endregion

        #region Current Guild
        public static string GuildId { get; set; }
        public static string GuildName { get; set; }
        public static List<Guild> Guilds { get; set; }
        public static List<Channel> Channels { get; set; }
        #endregion

        #region Users
        public static List<User> Users { get; set; } = new List<User>(); 
        #endregion

    }
}
