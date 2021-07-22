using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMorph.DataObjects
{
    public enum ChannelType
    {
        GuildText = 0,
        Dm = 1,
        GuildVoice = 2,
        GroupDm = 3,
        GuildCategory = 4,
        GuildNews = 5,
        GuildStore = 6,
        GuildNewsThread = 10,
        GuildPublicThread = 11,
        GuildPrivateThread = 12,
        GuildStageVoice = 13
    }
}
