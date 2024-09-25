

namespace FNSC.Data.StreamerBotClasses
{
    public class StreamerBotData
    {
        public string id { get; set; }
        public string name { get; set; }
        public StreamerBotArguments arguments { get; set; }
        public StreamerBotUser user { get; set; }
        public string command { get; set; }
        public int counter { get; set; }
        public int userCounter { get; set; }
        public string message { get; set; }

        public bool isCommand { get { return !string.IsNullOrEmpty(command); } }
    }

}
