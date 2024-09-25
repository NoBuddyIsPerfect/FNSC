using FNSC.Data.StreamerBotClasses;
using System;
using System.Collections.Generic;

namespace FNSC.Integrations.Classes
{
    /// <summary>
    /// Streamerbot action event arguments
    /// </summary>
    public class ActionsReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// List of actions
        /// </summary>
        public List<StreamerbotAction> Actions { get; } = new List<StreamerbotAction>();
    }
    
    public class ActionExecutedEventArgs : EventArgs
    {
        /// <summary>
        /// List of actions
        /// </summary>
        public StreamerBotActionMessage Action { get; set; }
    }
}
