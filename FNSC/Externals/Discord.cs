using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FNSC.Externals
{
    public class Discord
    {
        public static string WebhookUrl = "";
        private static string defaultUserAgent = "Streamer.Bot";
        private static string defaultUserName = "Streamer.Bot";
        private static HttpClient client;
        public static HttpClient Client
        {
            get
            {
                if (client == null)
                    client = new HttpClient();

                return client;
            }
        }
        public static void SendEmbed(string postContent, string author, string title, string url)
        {
            //https://discord.com/api/webhooks/1140563631352848496/hZ1gzUBLpBmYWULW0GtUBzXMagkSCWkXXnGJZilzcBCgPaUAyRhjVCdwF4jEplU6mN5h
            var WebHookId = "1140563631352848496";
            var WebHookToken = "hZ1gzUBLpBmYWULW0GtUBzXMagkSCWkXXnGJZilzcBCgPaUAyRhjVCdwF4jEplU6mN5h";


            string embedJson =
                " {\r\n  " +
                    $"\"content\": \"{postContent}\",\r\n  " +
                    "\"embeds\": " +
                    "[\r\n" +
                        "{\r\n      " +
                            $"\"title\": \"{title}\",\r\n      " +
                            $"\"description\": \"{author} is now live on Twitch\",\r\n      " +
                           "\"author\": " +
                           "{\r\n        " +
                                $"\"name\": \"{author}\"\r\n      " +
                           "}," +
                            $"\"url\": \"https://twitch.tv/{author.ToLower()}\",\r\n      " +
                            "\"color\": null,\r\n      " +
                            "\"image\": {\r\n        " +
                                $"\"url\": \"https://static-cdn.jtvnw.net/previews-ttv/live_user_{author.ToLower()}-440x248.jpg\"\r\n      " +
                            "}\r\n    " +
                         "}\r\n  " +
                     "],\r\n  " +
                     "\"attachments\": []\r\n" +
                 "}";

            string EndPoint = "https://discord.com/api/webhooks/1140563631352848496/hZ1gzUBLpBmYWULW0GtUBzXMagkSCWkXXnGJZilzcBCgPaUAyRhjVCdwF4jEplU6mN5h";

            var content = new StringContent(embedJson, Encoding.UTF8, "application/json");

            HttpResponseMessage msg = Client.PostAsync(EndPoint, content).Result;
        }

        public static string SendFile(
        string mssgBody,
        string filename,
        string filepath, string webhookurl)
        {
            WebhookUrl = webhookurl;
            if (string.IsNullOrEmpty(WebhookUrl))
                return "";
            string fileformat = "txt";
            string application = "application/text";
            // Read file data
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename", filename);
            postParameters.Add("fileformat", fileformat);
            postParameters.Add("file", new FormUpload.FileParameter(data, filename, application/*"application/msexcel"*/));

            postParameters.Add("username", defaultUserName);
            postParameters.Add("content", mssgBody);

            // Create request and receive response
            HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(WebhookUrl, defaultUserAgent, postParameters);

            // Process response
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            webResponse.Close();
            //Debug.Log("Discord: file success");

            //return string with response
            return fullResponse;
        }
    }
    public static class FormUpload //formats data as a multi part form to allow for file sharing
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());

            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData);
        }

        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                //Debug.LogWarning("request is not a http request");
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
}
