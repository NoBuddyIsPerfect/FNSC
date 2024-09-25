
using FNSC.Data.StreamerBotClasses;
using System;
using System.Collections.Generic;
using FNSC.Data.StreamerBotClasses;

namespace FNSC.Integrations.Classes
{
    /// <summary>
    /// Streamerbot action event arguments
    /// </summary>
    public class CommandEventArgs : EventArgs
    {
        /// <summary>
        /// List of actions
        /// </summary>
        public StreamerBotCommandMessagePart Command;
    }
    public class ChatMessageEventArgs : EventArgs
    {
        /// <summary>
        /// List of actions
        /// </summary>
        public StreamerBotChatMessagePart MessagePart;
    }
}
