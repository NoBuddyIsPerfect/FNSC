using DevExpress.XtraEditors.Accessibility;
using FNSC.Integrations.Classes;
using log4net;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using FNSC.Data.StreamerBotClasses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using FNSC.Data;


namespace FNSC.Externals
{
    /// <summary>
    /// StreamerBot client model
    /// </summary>
    public class StreamerbotClient : IDisposable
    {
        /// </summary>

        public event EventHandler<ActionsReceivedEventArgs> ActionsReceived;
        public event EventHandler<ActionExecutedEventArgs> ActionExecuted;
        public event EventHandler<CommandEventArgs> CommandReceived;
        public event EventHandler<ChatMessageEventArgs> ChatMessageReceived;

        private static readonly ILog logger = LogManager.GetLogger(typeof(StreamerbotClient));
        private WebSocket ws;
        private List<StreamerbotAction> actions = new List<StreamerbotAction>();
        /// <summary>
        /// Returns a list of StreamerbotActions
        /// </summary>
        public List<StreamerbotAction> Actions { get { return actions; } }


        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
        private Uri adress;
      private bool sendChatMessages = true;
        private bool isInitialStart = false;
        bool success = false;
        private bool showConnectionError = true;
        /// <summary>
        /// Connect to <paramref name="ip"/>:<paramref name="port"/>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Connect(string ip, string port, bool showConnectionError = true)
        {
            this.showConnectionError = showConnectionError;
            isInitialStart = true;
            if (ws == null)
            {
                if (string.IsNullOrEmpty(ip) && string.IsNullOrEmpty(port))
                {
                    return false;
                }

                adress = new Uri($"ws://{ip}:{port}/");

                ws = new WebSocket(adress.ToString());
            }

            SubscribeToWS();

            if (ws.ReadyState != WebSocketState.Open)
            {
                try
                {
                    ws.Connect();
                    while (ws != null && ws.ReadyState != WebSocketState.Open)
                        Thread.Sleep(500);
                }
                catch (Exception e)
                {
                    string t = e.Message;
                }
                //ExitEvent.WaitOne();
            }
            InitiateHeartBeat();

            SubscribeToEvents();
            isInitialStart = false;
            ConnectionSucceeded = true;
            return ws != null && ws.ReadyState == WebSocketState.Open;

        }
        private void SubscribeToWS()
        {
            //ws.ReconnectionHappened.Subscribe(s =>
            //{
            //    if (reconnectedAlready == false)
            //    {
            //        if ((s.Type == ReconnectionType.Error))
            //        {
            //            logger.Debug("Streamerbot disconnected, reconnected");
            //            // SubscribeToEvents();
            //            reconnectedAlready = true;
            //            MessageBox.Show("Reconnected to Streamer.Bot");
            //        }
            //        if (sendJoin)
            //        {
            //            SendMessage(LanguageStrings.Get("Chat_Msg_joinMessage"));
            //        }
            //    }
            //    else
            //    {
            //        reconnectedAlready = !reconnectedAlready;
            //    }
            //    ConnectionSucceeded = true;
            //});

            ws.OnMessage += (sender, e) =>
            {
                if (e.IsPing)
                    return;
                if (!e.Data.Contains("007") && !e.Data.ToLower().Contains("raw"))
                {
                    StreamerBotChatMessage action = JsonConvert.DeserializeObject<StreamerBotChatMessage>(e.Data);
                    if(action != null && action.@event != null)
                        switch (action.@event.source)
                        {
                            case StreamerBotEventSource.Twitch:
                                if (action.@event.type.ToLower() == "chatmessage")
                                {
                                    StreamerBotChatMessagePart Obj =
                                        JsonConvert.DeserializeObject<StreamerBotChatMessagePart>(
                                            action.data.ToString());
                                    ChatMessageReceived?.Invoke(this, new ChatMessageEventArgs() { MessagePart = Obj });
                                }
                                break;
                            case StreamerBotEventSource.Command:
                                StreamerBotCommandMessagePart commandObj =
                                    JsonConvert.DeserializeObject<StreamerBotCommandMessagePart>(
                                        action.data.ToString());
                                commandObj.message = commandObj.message.Replace("󠀀", "");
                                if (commandObj.command == "1" || commandObj.command == "2")
                                    commandObj.message = commandObj.command;
                                CommandReceived?.Invoke(this, new CommandEventArgs() { Command = commandObj });
                                break;
                        }
                    //if (action.data.isCommand)
                    //{ 
                    //    string test = action.data.command; 
                    //}
                }
                
                success = ws.IsAlive;
                //if (!e.Data.ToLowerInvariant().Contains("\"id\":\"0\",\"events\":", StringComparison.InvariantCulture)
                //   && e.Data.ToLowerInvariant().Contains("\"source\":\"command\"", StringComparison.InvariantCulture))
                //{
                //    string t = "";
                //}
                if (!e.Data.ToLowerInvariant().Contains("\"id\":\"0\",\"events\":", StringComparison.InvariantCulture)
                && e.Data.ToLowerInvariant().Contains("\"source\":\"raw\"", StringComparison.InvariantCulture))
                {
                    StreamerBotActionMessage action = JsonConvert.DeserializeObject<StreamerBotActionMessage>(e.Data);
                    OnActionExecuted(new ActionExecutedEventArgs(){Action = action });
                    //if (action.@event.type.ToLowerInvariant() == "subaction")
                    //    logger.Debug("Subaction execution resceived: " + action.data.name);
                    //else
                    //    logger.Debug("Action execution resceived: " + action.data.name);
                }
                if (!e.Data.ToLowerInvariant().Contains("\"id\":\"123\",\"events\":", StringComparison.InvariantCulture)
                    && e.Data.ToLowerInvariant().Contains("count", StringComparison.InvariantCulture) && e.Data.ToLowerInvariant().Contains("actions", StringComparison.InvariantCulture))
                {
                    StreamerbotActionList list = JsonConvert.DeserializeObject<StreamerbotActionList>(e.Data);
                    actions.AddRange(list.actions);
                    ActionsReceivedEventArgs args = new();
                    args.Actions.AddRange(list.actions);
                    OnActionsReceived(args);
                };
                //if (!e.Data.ToLowerInvariant().Contains("\"id\":\"123\",\"events\":", StringComparison.InvariantCulture)
                //    && e.Data.ToLowerInvariant().Contains("custom", StringComparison.InvariantCulture))
                //{
                //    StreamerBotCustomMessage @event = JsonConvert.DeserializeObject<StreamerBotCustomMessage>(e.Data);
                //    logger.Debug("Custom Event received: " + @event.Data.Data);
                //    if (@event.Data.Data == "TimerEnd")
                //    {

                //    }
                //};
                //if (e.IsPing || e.Data.ToLowerInvariant().Contains("\"id\":\"0\",\"events\":", StringComparison.InvariantCulture))
                //{
                //    logger.Debug("Heartbeat successful");
                //    ConnectionSucceeded = true;

                //};
                string t = e.Data;
            };
            ws.EmitOnPing = true;

            ws.OnError += (sender, e) =>
            {
                string t = e.Message;

            };
            ws.OnClose += (sender, e) =>
            {
                // sb close: 1005, clean
                //sb closed on start: 1006, false, text: An exception has occurred while connecting.
                //sb crashes: 1006, false, text: An exception has occurred while receiving.
                if (e.WasClean)
                    reconnectWanted = MessageBox.Show("Connection to Streamer.Bot lost! Do you want to try to reconnect?", "Connection lost", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                if (!e.WasClean && e.Reason.ToLowerInvariant() == "an exception has occurred while connecting." && !reconnectWanted && showConnectionError)
                {
                    MessageBox.Show("Could not connect to Streamer.Bot\r\nPlease make sure that IP and port are correct,\rthat Streamer.Bot and its websocket server are running!\rThen restart FNSC!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    ws = null;
                }

                if (!reconnectWanted && !e.WasClean && e.Reason.ToLowerInvariant() == "an exception has occurred while receiving.")
                {
                    reconnectWanted = MessageBox.Show("Connection to Streamer.Bot unexpectedly lost! Do you want to try to reconnect?", "Connection lost", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                }


            };
            //ws.DisconnectionHappened.Subscribe(async e =>
            //{
            //    logger.Debug("Streamerbot disconnected, reconnecting....");

            //    if ((e.Type == DisconnectionType.ByServer && e.CloseStatus == WebSocketCloseStatus.NormalClosure))
            //    {
            //        MessageBox.Show("Connection closed by Streamer.Bot.\r\nTrying to reconnect...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //    }
            //    if (e.Type == DisconnectionType.Lost && e.CloseStatus != WebSocketCloseStatus.NormalClosure)
            //    {
            //        MessageBox.Show("Connection to Streamer.Bot lost.\r\nTrying to reconnect...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //    }
            //    if (e.Type == DisconnectionType.Error && e.Exception.MessagePart == "Unable to connect to the remote server")
            //    {
            //        MessageBox.Show("Could not connect to Streamer.Bot\r\nPlease make sure that IP and port are correct,\rthat Streamer.Bot and its websocket server are running!\rThen restart FNSC!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //        e.CancelReconnection = true;
            //    }
            //    ConnectionSucceeded = false;
            //    //if(ws.IsRunning || ws.IsStarted)
            //    //ws.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "");
            //    //ws.Reconnect();
            //    //Thread.Sleep(5000);
            //});

        }

        private bool reconnectWanted = false;
        public bool ConnectionSucceeded { get; set; } = false;
        /// <summary>
        /// Fires <see cref="ActionsReceived"/>
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnActionsReceived(ActionsReceivedEventArgs e)
        {
            logger.Debug($"Received actions");
            if (actions.Any())
                actions.Clear();
            actions.AddRange(e.Actions);
            ActionsReceived?.Invoke(this, e);
        }
        protected virtual void OnActionExecuted(ActionExecutedEventArgs e)
        {
            ActionExecuted?.Invoke(this, e);
        }
        System.Threading.Timer timer;

        private void InitiateHeartBeat()
        {
            timer = new System.Threading.Timer(async (state) =>
            {
                if (ws != null && ws.ReadyState != WebSocketState.Open && reconnectWanted)
                {
                    ws.Connect();

                    while (ws.ReadyState != WebSocketState.Open)
                        Thread.Sleep(500);
                    SubscribeToEvents();


                }
                if (ws != null && ws.ReadyState == WebSocketState.Open)
                {
                    ws.Ping();
                }


            }, null, 1000, 10000);
            //{ "request": "GetEvents", "id": "<MessagePart id>"}

        }


        /// <summary>
        /// Send GetActions request through web socket
        /// </summary>
        public void GetActions()
        {
            logger.Debug($"Getting actions");
            if (ws != null && ws.ReadyState != WebSocketState.Open)
            {
                ws.Connect();
            }
            if (ws != null && ws.ReadyState == WebSocketState.Open)
            {
                ws.Send("{\"request\":\"GetActions\",\"id\":\"123\"}");
            }
        }


        /// <summary>
        /// Send GetActions request through web socket
        /// </summary>
        public void SubscribeToEvents()
        {
            logger.Debug($"Subscribing to Streamer.Bot events");
            if (ws != null && ws.ReadyState != WebSocketState.Open)
            {
                ws.Connect();
            }
            if (ws != null && ws.ReadyState == WebSocketState.Open)
            {
                string req = "{\"request\": \"GetEvents\",\"id\": \"9999\",}";
//                ws.Send(req);


                string requestString = "{  \"request\": \"Subscribe\",  \"id\": \"007\",  \"events\": {    \"twitch\": [      \"BotWhisper\",      \"Whisper\",  \"ChatMessage\"       ],\"command\": [\"Triggered\"],\"raw\": [\"Action\"],  },}";
                ws.Send(requestString);
            }
        }

        /// <summary>
        /// Send DoAction request with given arguments through web socket
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public void ExecuteAction(string guid, string name, Dictionary<string, string> args)
        {
            if (ws != null && ws.ReadyState != WebSocketState.Open)
            {
                ws.Connect();
            }
            if (ws != null && ws.ReadyState == WebSocketState.Open)
            {
                string argsString = JsonConvert.SerializeObject(args);
                string reqString = @"{""request"": ""DoAction"",""action"": { ""id"": """ + guid + @""", ""name"": """ + name + @""" }, ""args"":  " + argsString + @", ""id"":""123""}";
                logger.Debug($"Executing action: {reqString}");
                ws.Send(reqString);
                
            }
        }
        /// <summary>
        /// Sends a chat command to the specified action
        /// </summary>
        /// <param name="chatActionGuid"></param>
        /// <param name="chatActionName"></param>
        /// <param name="MessagePart"></param>
        //public void SendChatMessage(string chatActionGuid, string chatActionName, string MessagePart)
        //{
        //    ExecuteAction(chatActionGuid, chatActionName, new Dictionary<string, string>() { { "MessagePart", MessagePart } });    
        //}


        /// <summary>
        /// Start connection
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool TestConnection(string ip, string port)
        {
            logger.Debug($"Testing Streamerbot connection");
            if (ws == null)
            {
                ws = new WebSocket($"ws://{ip}:{port}/");
            }

            bool success;

            if (ws.ReadyState != WebSocketState.Open)
            {
                ws.Connect();
            }

            success = ws.ReadyState == WebSocketState.Open;
            logger.Debug($"SB connection is {success}");
            if (ws.ReadyState == WebSocketState.Open)
            {
                ws.CloseAsync(CloseStatusCode.Normal, "");
            }

            return success;
        }

        /// <summary>
        /// Close web socket connection
        /// </summary>
        public bool CloseConnection()
        {
            logger.Debug($"Closing Streamer.Bot connection");
            if (ws != null)
            {
                ws.CloseAsync(CloseStatusCode.Normal, "");
                if (adress != null)
                    ws = new WebSocket(adress.ToString());
                return ws.ReadyState == WebSocketState.Open;
            }
            ConnectionSucceeded = false;
            return true;
        }

        /// <summary>
        /// <see cref="ExecuteAction(string, string, Dictionary{string, string})"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void ExecuteAction(string id, string name)
        {
            ExecuteAction(id, name, new Dictionary<string, string>());
        }

        /// <summary>
        /// Wheter connection is still alive
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            try
            {
                if (ws != null)
                    ws.Send("ping");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        #region botInterface

        public bool GetEventArgObject(object e, out StreamerBotCommandMessagePart message, out Type underlyingType)
        {
            underlyingType = null;
            message = null;
            if (e is StreamerBotCommandMessage m)
            {
                underlyingType = m.GetType();
                message = m.data;
            }
            //else if (e is OnWhisperReceivedArgs w)
            //{
            //    underlyingType = w.GetType();
            //    command = w.WhisperMessage;
            //}
            else
            {
                return false;
            }
            return message != null && underlyingType != null;
        }

        public void SendMessage(string message)
        {
            if (sendChatMessages && !string.IsNullOrEmpty(Properties.Settings.Default.SendMessageActionID) && !string.IsNullOrEmpty(Properties.Settings.Default.SendMessageActionName))
                ExecuteAction(Properties.Settings.Default.SendMessageActionID, Properties.Settings.Default.SendMessageActionName, new Dictionary<string, string>() { { "MessagePart", message } });

        }
        
        public void SendWhisper(string user, string message)
        {
            if (sendChatMessages && !string.IsNullOrEmpty(Properties.Settings.Default.SendWhisperActionID) && !string.IsNullOrEmpty(Properties.Settings.Default.SendWhisperActionName))
                ExecuteAction(Properties.Settings.Default.SendWhisperActionID, Properties.Settings.Default.SendWhisperActionName, new Dictionary<string, string>() { { "MessagePart", message }, {"username", user} });

        }

     


        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}
