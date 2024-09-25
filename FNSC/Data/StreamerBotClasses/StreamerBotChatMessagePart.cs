using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNSC.Data.StreamerBotClasses
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Badge
    {
        public string name { get; set; }
        public string version { get; set; }
        public string imageUrl { get; set; }
        public string info { get; set; }
    }

    public class MessagePart
    {
        public bool @internal { get; set; }
        public string msgId { get; set; }
        public string clientNonce { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public int role { get; set; }
        public bool subscriber { get; set; }
        public string displayName { get; set; }
        public string color { get; set; }
        public string channel { get; set; }
        public string message { get; set; }
        public bool isHighlighted { get; set; }
        public bool isMe { get; set; }
        public bool isCustomReward { get; set; }
        public bool isAnonymous { get; set; }
        public bool isReply { get; set; }
        public int bits { get; set; }
        public bool firstMessage { get; set; }
        public bool hasBits { get; set; }
        public List<object> emotes { get; set; }
        public List<object> cheerEmotes { get; set; }
        public List<Badge> badges { get; set; }
        public int monthsSubscribed { get; set; }
        public bool isTest { get; set; }
    }

    public class StreamerBotChatMessagePart
    {
        public MessagePart Message { get; set; }
    }


}
