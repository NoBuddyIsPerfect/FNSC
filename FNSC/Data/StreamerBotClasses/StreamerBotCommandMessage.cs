namespace FNSC.Data.StreamerBotClasses
{
    public class StreamerBotCommandMessage
    {

        public string timeStamp { get; set; }
        public StreamerBotEvent Event { get; set; }
        public StreamerBotCommandMessagePart data { get; set; }

    }

}
