namespace FNSC.Data.StreamerBotClasses
{
    public class StreamerBotCommandMessagePart
    {
        public string command { get; set; }
        public int counter { get; set; }
        public int userCounter { get; set; }
        public string message { get; set; }
        public string completeMessage { get => command + " " + message; }
        public StreamerBotUser user { get; set; }


    }

}
