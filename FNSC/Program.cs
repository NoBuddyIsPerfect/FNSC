using DevExpress.CodeParser;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;


namespace FNSC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string log4netConfig = (Process.GetCurrentProcess().MainModule.FileName + ".config").Replace("exe", "dll");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netConfig));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length > 0 && args.Any(a => a.Contains("debug")))
                Application.Run(new frmManagement(true));
            else
                Application.Run(new frmManagement());
        }
    }




    //public partial class Form1 : Form
    //{
    //    private static readonly StreamerbotClient streamerbotClient = new();
    //    private static readonly ObsClient obsClient = new();

    //    public Form1()
    //    {
    //        InitializeComponent();
    //    }

    //    private async void Form1_Load(object sender, EventArgs e)
    //    {
    //        streamerbotClient.ActionsReceived += StreamerbotClient_ActionsReceived;

    //        if (await streamerbotClient.Connect("127.0.0.1", "6969", "", ""))
    //        {
    //            // StreamerbotClient.GetActions();
    //            streamerbotClient.ExecuteAction("537f127a-5066-4166-a32d-b57e15fb5786", "NBSC TEST", new Dictionary<string, string>() { { "rawInput", "Kommt vom Programm" } });
    //        }
    //        ChampionshipContext championshipContext = new ChampionshipContext();
    //        Game? game = championshipContext.FindAsync<Game>(1).Result;

    //    }

    //    private void StreamerbotClient_ActionsReceived(object? sender, FNSC.Integrations.Classes.ActionsReceivedEventArgs e)
    //    {
    //        string? t = e?.Actions?.FirstOrDefault()?.name;
    //    }
    //}




    // private void RoundForm_Load(object sender, EventArgs e)
    //    {
    //        var embed = "<html><head>" +
    //"<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
    //"</head><body>" +
    //"<iframe width=\"300\" src=\"{0}\"" +
    //"frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
    //"</body></html>";
    //var url = "https://www.youtube.com/embed/L6ZgzJKfERM";
    //        this.webBrowser1.DocumentText = string.Format(embed, url);
    //}
}
