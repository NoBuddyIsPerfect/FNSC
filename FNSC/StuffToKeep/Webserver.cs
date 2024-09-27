using EmbedIO;
using log4net;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNSC.tuffToKeep
{
    public class Webserver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Webserver));

        public static async Task Start()
        {
            var url = "http://localhost:9696/";
            using (var server = CreateWebServer(url))
            {
                // Once we've registered our modules and configured them, we call the RunAsync() method.
                await server.RunAsync();

                //var browser = new System.Diagnostics.Process()
                //{
                //    StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
                //};
                //browser.Start();
                // Wait for any key to be pressed before disposing of our web server.
                // In a service, we'd manage the lifecycle of our web server using
                // something like a BackgroundWorker or a ManualResetEvent.
                // Console.ReadKey(true);
            }
        }
        // Create and configure our web server.
        public static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => { 
                o.WithUrlPrefix(url);
                o.WithMode(HttpListenerMode.EmbedIO);
                
            })
                .WithStaticFolder("/", Properties.Settings.Default.WebserverPath, false);
            //.WithStaticFolder("/", "E:\\SB Extensions\\SongChampionship", false, o => { o.DefaultExtension = "html"; });
            server.HandleHttpException(async (context, exception) =>
            {
                log.Error(exception);
            });
            // Listen for state changes.
            server.StateChanged += (s, e) => Console.WriteLine($"WebServer New State - {e.NewState}");

            return server;
        }
    }
}
