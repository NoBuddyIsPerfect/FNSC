using System;

namespace FNSC.Data.StreamerBotClasses
{
    public class StreamerBotActionMessage
    {
        public DateTime timeStamp { get; set; }
        public StreamerBotEvent @event { get; set; }
        public Data data { get; set; }
    }
    public class StreamerBotChatMessage
    {
        public DateTime timeStamp { get; set; }
        public StreamerBotEvent @event { get; set; }
        public object data { get; set; }
    }
    public class Arguments
    {
        public object value { get; set; }
        public string sdButtonUuid { get; set; }
        public string sdButtonId { get; set; }
        public string actionId { get; set; }
        public string actionName { get; set; }
        public string eventSource { get; set; }
        public string runningActionId { get; set; }
        public DateTime actionQueuedAt { get; set; }

        public string coinFlip { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public string actionId { get; set; }
        public string name { get; set; }
        public Arguments arguments { get; set; }
    }


}
