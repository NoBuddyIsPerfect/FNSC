using ObsWebsocketDotNet;
using ObsWebsocketDotNet.Types;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;


namespace FNSC.Externals
{
    /// <summary>
    /// Obs client model
    /// </summary>
    public class ObsClient
    {
        /// <summary>
        /// Websocket instance
        /// </summary>
        protected OBSWebsocket obs { get; init; }
        public event EventHandler Connected;
        /// <summary>
        /// 
        /// </summary>
        public ObsClient()
        {
            obs = new OBSWebsocket();

            obs.Connected += Obs_Connected;
            obs.Disconnected += Obs_Disconnected1;
        }

        private void Obs_Disconnected1(object sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            
        }

        /// <summary>
        /// Current scene
        /// </summary>
        public string CurrentScene => !obs.IsConnected ? "" : obs.GetCurrentProgramScene();
        /// <summary>
        /// Get a list of scenes
        /// </summary>
        /// <returns></returns>
        public List<SceneBasicInfo> GetScenes()
        {
            if (!obs.IsConnected)
            {
                return new List<SceneBasicInfo>();
            }

            List<SceneBasicInfo> scenes = obs.ListScenes();
            return scenes;
        }
        /// <summary>
        /// Get a list of sources
        /// </summary>
        /// <returns></returns>
        public List<SourceActiveInfo> GetSources()
        {
            //if (!obs.IsConnected)
            //{
            return new List<SourceActiveInfo>();
            //}

            // List<SourceInfo> scenes = obs.getso();
            //return scenes;
        }
        /// <summary>
        /// Hide given item in given scene
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="itemName"></param>
        public void HideItem(string sceneName, string itemName)
        {
            if (!obs.IsConnected)
            {
                return;
            }

            int id = 0;
            if (items.ContainsKey(itemName))
                id = items[itemName];
            else
            id = obs.GetSceneItemList(sceneName).First(i => i.SourceName == itemName).ItemId;
            if(id > 0)
                obs.SetSceneItemEnabled(sceneName, id, false);
        }

        private Dictionary<string, int> items = new();
        public void GetSceneItems()
        {
            if (!obs.IsConnected)
            {
                return;
            }

            var item = obs.GetSceneItemList("[NS] Song Championship");
            foreach (SceneItemDetails details in item)
            {
                items.Add(details.SourceName, details.ItemId);
            }

        }
        /// <summary>
        /// Show given item in given scene
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="itemName"></param>
        public void ShowItem(string sceneName, string itemName)
        {
            if (!obs.IsConnected)
            {
                return;
            }

            try
            {
                var list = obs.GetSceneItemList(sceneName);
                SceneItemDetails item = list.FirstOrDefault(i => i.SourceName == itemName);
                if (item != null)
                    obs.SetSceneItemEnabled(sceneName, item.ItemId, true);
            }
            catch(Exception e)
            {
                string t = e.Message;
            }
        }
        public void EnableFilter(string source, string filter)
        {
            if (!obs.IsConnected)
            {
                return;
            }

            try
            {
                obs.SetSourceFilterEnabled(source, filter, true);
                
            }
            catch(Exception e)
            {
                string t = e.Message;
            }
        }

        /// <summary>
        /// Modify a source in a scene to execute <paramref name="action"/>
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="sourceName"></param>
        /// <param name="action"></param>
        public void ModifySource(string sceneName, string sourceName, string action)
        {
            if (!obs.IsConnected)
            {
                return;
            }
            SceneBasicInfo scene = obs.ListScenes().FirstOrDefault(s => s.Name == sceneName);
            SceneItemDetails item = obs.GetSceneItemList(sceneName).FirstOrDefault(i => i.SourceName == sourceName);
            if (item != null)
            {
                switch (action?.ToLowerInvariant())
                {
                    case "show":
                        obs.SetSceneItemEnabled(sceneName, item.ItemId, true);
                        break;
                    case "hide":
                        obs.SetSceneItemEnabled(sceneName, item.ItemId, false);
                        break;
                    default:
                        bool isActive = obs.GetSceneItemEnabled(sceneName, item.ItemId);
                        obs.SetSceneItemEnabled(sceneName, item.ItemId, !isActive);
                        break;
                }
            }
        }

        public void SetText(string sceneItem, string text)
        {
            if (!obs.IsConnected)
            {
                return;
            }
            JObject settings =
                JObject.Parse(
                    "{\r\n    \"inputName\": \""+sceneItem+"\",\r\n    \"inputSettings\": {\r\n      \"text\": \""+text+"\"\r\n    }}");
            InputSettings inputSettings = new InputSettings(settings);
            obs.SetInputSettings(inputSettings);
        }

        /// <summary>
        /// Switch to <paramref name="sceneName"/>
        /// </summary>
        /// <param name="sceneName"></param>
        public void SwitchToScene(string sceneName)
        {
            //if (!obs.IsConnected)
            //{
            //    return;
            //}
            //obs.SetCurrentScene(sceneName);
        }



        private void Obs_Connected(object sender, EventArgs e)
        {
            Connected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Connect to <paramref name="ip"/> with <paramref name="password"/>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Connect(string ip, string password)
        {
            if (!obs.IsConnected)
            {
                try
                {

                     obs.ConnectAsync(ip, password);
                }
                catch (AuthFailureException)
                {
                    return false;
                }
                catch (ErrorResponseException)
                {
                    return false;
                }
                catch (SocketException)
                {
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Start the connection to <paramref name="ip"/>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TestConnection(string ip, string password)
        {
            if (!obs.IsConnected)
            {
                try
                {
                    obs.ConnectAsync(ip, password);
                    obs.Disconnect();
                }
                catch (AuthFailureException)
                {
                    return false;
                }
                catch (ErrorResponseException)
                {
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Disconnect from the websocket connection
        /// </summary>
        public void Disconnect()
        {

            if (obs.IsConnected)
            {
                obs.Disconnect();
            }
        }

        /// <summary>
        /// Wheter connection is still alive
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return obs.IsConnected;
        }
    }

}

namespace ObsWebsocketDotNet.Types
{
    ///// <summary>
    ///// Scene item model
    ///// <para>Source information returned by GetSourcesList</para>
    ///// </summary>
    //public class MySceneItem : SceneItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="item"></param>
    //    public MySceneItem(SceneItem item)
    //    {
    //        if (item != null)
    //        {
    //            AudioVolume = item.AudioVolume;
    //            GroupChildren = item.GroupChildren;
    //            Height = item.Height;
    //            ID = item.ID;
    //            InternalType = item.InternalType;
    //            Locked = item.Locked;
    //            ParentGroupName = item.ParentGroupName;
    //            Render = item.Render;
    //            SourceHeight = item.SourceHeight;
    //            SourceName = item.SourceName;
    //            SourceWidth = item.SourceWidth;
    //            Width = item.Width;
    //            XPos = item.XPos;
    //            YPos = item.YPos;
    //        }
    //    }
    //    /// <summary>
    //    /// Source name
    //    /// </summary>
    //    public string Name => SourceName;

    //}

    ///// <summary>
    ///// Obs Scene model
    ///// </summary>
    //public class MyObsScene : ObsScene
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="item"></param>
    //    public MyObsScene(ObsScene item)
    //    {
    //        if (item != null)
    //        {
    //            Name = item.Name;
    //            Items = item.Items;
    //        }
    //    }

    //    /// <summary>
    //    /// Name
    //    /// </summary>
    //    public string DisplayName => Name;

    //}


}
