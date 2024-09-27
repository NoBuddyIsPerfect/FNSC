using Newtonsoft.Json;
using FNSC.Data;
using FNSC.Externals.YouTube;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace FNSC.Externals
{
    internal class YouTubeClient
    {
        public static Song GetYTVideoDetails(string url, string ytApiKey)
        {
            Song foundSoung = new Song()
            {
                Description = "NO TITLE",
                Channel = "NO CHANNEL",
                Length = new TimeSpan(0),
                IsBlocked = false,
                Url = new Uri(url)
            };

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri) && uri != null)
            {
                string code = "";
                string startTime = "";
                NameValueCollection param = HttpUtility.ParseQueryString(uri.Query);
                if (param.Count > 0)
                {
                    code = param.Get("v") ?? "";

                    startTime = param.Get("t")?.TrimEnd('s') ?? "";

                }

                if (string.IsNullOrEmpty(code))
                    code = uri.Segments[1];
                if (string.IsNullOrEmpty(startTime) && uri.Segments.Length > 2)
                    startTime = uri.Segments[2];
                if(!string.IsNullOrEmpty(startTime) && int.TryParse(startTime, out int time))
                    foundSoung.InitialStarttime = time;
                foundSoung.Code = code;
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                string requesturl = "https://www.googleapis.com/youtube/v3/videos?id=" + foundSoung.Code + "&part=contentDetails&part=snippet&key=" + ytApiKey;
                var content = client.GetStringAsync(requesturl).Result;


                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(content);
                if (myDeserializedClass.items[0] != null)
                {
                    foundSoung.Description = myDeserializedClass.items[0].snippet.title;
                    foundSoung.Channel = myDeserializedClass.items[0].snippet.channelTitle;
                    foundSoung.Length = XmlConvert.ToTimeSpan(myDeserializedClass.items[0].contentDetails.duration);
                    foundSoung.IsBlocked = (myDeserializedClass.items[0].contentDetails.regionRestriction != null 
                                            && myDeserializedClass.items[0].contentDetails.regionRestriction.blocked != null 
                                            && myDeserializedClass.items[0].contentDetails.regionRestriction.blocked.Contains("DE")) 
                                           || !string.IsNullOrEmpty(myDeserializedClass.items[0].contentDetails.contentRating.ytRating);
                    //if (span.Minutes > 9)
                    //{
                    //    CPH?.SendMessage("Sorry, @" + User + ", that Song is too long! (>5 min)", true);
                    //    return true;
                    //}
                    //else if (span.Minutes < 2)
                    //{
                    //    CPH?.SendMessage("Sorry, @" + User + ", that Song is too short! (<2 min)", true);
                    //    return true;
                    //}
                }
                return foundSoung;
            }
        }
    }
}
