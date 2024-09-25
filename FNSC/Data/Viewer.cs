using FNSC.Data.StreamerBotClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FNSC.Data;

namespace FNSC.Data
{
    [DebuggerDisplay("{display, id}")]
    public class Viewer
    {
        public string id { get; set; }

        public string name { get; set; }
        public string display { get; set; }
        public bool subscribed { get; set; }
        public int role { get; set; }

        public string type { get; set; }

        public override string ToString()
        {
            return $"{display} ({id})";
        }

        public static Viewer Convert(StreamerBotUser user)
        {
            Viewer existing = ChampionshipContext.ContextInstance.Viewers.FirstOrDefault(v => v.id == user.id);
            if (existing != null)
                return existing;

            return new Viewer()
            {
                id = user.id,
                name = user.name,
                display = user.display,
                role = user.role,
                type = user.type,
                subscribed = user.subscribed,

            };
        }

        public static Viewer Convert(string id, string name, string display, bool subscribed, int role, string type)
        {
            Viewer existing = ChampionshipContext.ContextInstance.Viewers.FirstOrDefault(v => v.id == id);
            if (existing != null)
                return existing;
            return new Viewer()
            {
                id = id,
                name = name,
                display = display,
                role = role,
                type = type,
                subscribed = subscribed,

            };
        }
    }
}
