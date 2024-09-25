using OBSWebsocketDotNet.Types;

namespace FNSC.Integrations.Classes
{
    /// <summary>
    /// OBS scene model
    /// </summary>
    public class GCOBSScene : ObsScene
    {
        
        /// <summary>
        /// Name of scene
        /// </summary>
        public string DisplayName => Name;
    }
}
