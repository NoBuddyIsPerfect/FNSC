namespace FNSC.Data.StreamerBotClasses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class StreamerBotArguments
    {
        public string actionId { get; set; }
        public string user { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string userType { get; set; }
        public bool isSubscribed { get; set; }
        public bool isModerator { get; set; }
        public bool isVip { get; set; }
        public string eventSource { get; set; }
        public string broadcastUser { get; set; }
        public string broadcastUserName { get; set; }
        public int broadcastUserId { get; set; }
        public bool broadcasterIsAffiliate { get; set; }
        public bool broadcasterIsPartner { get; set; }
        public string runningActionId { get; set; }
    }

}
