using CefSharp.WinForms;
using CefSharp;
using FNSC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FNSC.tuffToKeep;
using EmbedIO;
using System.Runtime.InteropServices;
using log4net;
using FNSC.Helpers;
using DevExpress.CodeParser;
using DevExpress.Utils.Layout;
using FNSC.tuffToKeep;
using static FNSC.Helpers.MouseHelpers;
using Timer = System.Windows.Forms.Timer;
using System.Timers;
using DevExpress.XtraGrid.Views.Grid;
using FNSC.Integrations.Classes;
using FNSC.Externals;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ServiceModel.Channels;
using FNSC.Classes;

namespace FNSC
{
    public partial class frmChampionship : Form
    {
        public static frmChampionship Instance { get; private set; }


        int previewTime = 30;
        private readonly int startLeft = 25;
        private readonly int startRight = 0;
        private Game game;
        private ObsClient obs;
        private StreamerbotClient bot;

        public frmChampionship(Game game, ObsClient obsObs, StreamerbotClient bot)
        {
            Webserver.Start();
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.game = game;
            game.PreparePlayers();
            this.obs = obsObs;
            this.bot = bot;
            GameOnStartNextBattle(null, null);
            browserLeft.Load("http://localhost:9696/left.html");
            browserRight.Load("http://localhost:9696/right.html");
            // Add the browser to the form
            this.WindowState = FormWindowState.Maximized;
            TogglePlayButtons(true);
            btnCancelPreview.Enabled = false;
            GridControl1.DataSource = game.SubmittedSongs;
            numTotalPreviewTime.Value = game.PreviewTime;
            numTotalVotingTime.Value = game.VotingTime;
            bot.ActionExecuted += BotOnActionExecuted;
            bot.CommandReceived +=BotOnCommandReceived;
            game.VoteReceived += GameOnVoteReceived;
            game.WinnerFound += GameOnWinnerFound;
            game.StartNextBattle += GameOnStartNextBattle;
            game.RoundWinnerFound += GameOnRoundWinnerFound;
            Instance = this;
          
        
        }

        private void BotOnCommandReceived(object sender, CommandEventArgs e)
        {
            if (e.Command.command == "1" || e.Command.command == "2")
            {
                GridControl1.DataSource = game.SubmittedSongs;
                GridControl1.RefreshDataSource();
                GridControl1.Invalidate();
                string text = $"{e.Command.user.display} voted {e.Command.command}";
                 LogToTextbox(text);
            }

        }

        private void GameOnRoundWinnerFound(object sender, Song e)
        {
            obs.SetText(Properties.Settings.Default.WinnerTopSource, $"{e.Channel} -\n{(e.Description.Length < 30 ? e.Description : e.Description.Substring(0, 30))}");
            obs.SetText(Properties.Settings.Default.WinnerBottomSource, $"by\n{e.Viewer.display}");
            winnerHasBeenFound = true;
            winnerSong = e;
            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
        }

        void gridView1_CustomUnboundColumnData(object sender,
            DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = e.ListSourceRowIndex + 1;
        }

        private void GameOnStartNextBattle(object sender, EventArgs e)
        {
            if (game == null || game.CurrentRound == null || game.CurrentRound.CurrentBattle == null)
                return;
            string headerText = "Round\n\r" + game.CurrentRound.RoundNumber + "\n\rBattle\n\r" + game.CurrentRound.CurrentBattle.Position + "/" + (game.CurrentRound.Battles.Count + game.CurrentRound.FinishedBattles.Count);
            if ((game.CurrentRound.Battles.Count + game.CurrentRound.FinishedBattles.Count) == 4)
                headerText = "Quarterfinal";
            else if ((game.CurrentRound.Battles.Count + game.CurrentRound.FinishedBattles.Count) == 2)
                headerText = "Semifinal";
            else if (game.CurrentRound.BattleCount == 1)
            {
                headerText = "Final";
                obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.AllVotesSource);
            }
            obs.SetText(Properties.Settings.Default.RoundHeaderSource, headerText);
        }


        private delegate void AppendLogTextboxDelegate(string message);
        private void GameOnVoteReceived(object sender, VoteReceivedEventArgs e)
        {
            string text = $"{e.Viewer.display} voted {e.Vote}";
            // LogToTextbox(text);
        }

        private Song winnerSong;
        
        private void GameOnWinnerFound(object sender, Song e)
        {
            obs.EnableFilter(Properties.Settings.Default.MainChampionshipScene, Properties.Settings.Default.CornerFilterName); 
            obs.SetText(Properties.Settings.Default.WinnerTopSource, $"{e.Channel} -\n{(e.Description.Length < 30 ? e.Description : e.Description.Substring(0, 30))}");
            obs.SetText(Properties.Settings.Default.WinnerBottomSource, $"by\n{e.Viewer.display}");

              obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.MainTextSource);
                obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.AllVotesSource);
                obs.HideItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.RightPlayerSource);
                obs.HideItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.LeftPlayerSource);
                obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.VsSource);
                obs.HideItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.RoundHeaderSource);
                obs.ShowItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.WinnerTopSource);
                obs.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerSource);

                obs.ShowItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.WinnerBottomSource);
                
          
         
        }
    public void LogToTextbox(string message)
        {
            if (txtLogOutput.InvokeRequired)
                txtLogOutput.Invoke(new AppendLogTextboxDelegate(WriteTextToLogControl), message);
            else
                txtLogOutput.AppendText(Environment.NewLine + message);
        }
        public void WriteTextToLogControl(string message)
        {
            txtLogOutput.AppendText(Environment.NewLine + message);
        }

        private void BotOnActionExecuted(object sender, ActionExecutedEventArgs e)
        {
            if (e.Action.data.name == "Next Battle")
            {
                btnNextRound_Click(null, null);
            }
        }

        static frmChampionship()
        {
            Webserver.Start();


        }


        private void TogglePlayButtons(bool enable)
        {
            btnPlayLeft.Enabled = btnPlayRight.Enabled = btnPreview.Enabled = btnVoting.Enabled = enable;
        }

        private void btnPlayRight_Click(object sender, EventArgs e)
        {
            ClickRight();

        }

        private double GetPlayerTime(Players player)
        {
            var browser = player == Players.Left ? browserLeft : browserRight;
            var result = browser.GetMainFrame().EvaluateScriptAsync("player.getCurrentTime()")
                .ContinueWith(t =>
                {
                    var result = t.Result;
                    return result;
                }).Result;
            return (double)result.Result;
        }

        private void ClickRight()
        {
            MousePoint p = new MousePoint(1441, 280);
            //MousePoint p = new MousePoint(rightPlayerCenterX, rightPlayerCenterY);
            PerformMouseClick(p);
            LogToTextbox("Right Click");
        }

        private void PerformMouseClick(MousePoint p)
        {
            this.Activate();
            //this.Cursor = new Cursor(Cursor.Current.Handle);
            int x = (int)Cursor.Position.X;
            int y = (int)Cursor.Position.Y;
            MouseHelpers.SetCursorPosition(p);
            MouseHelpers.MouseEvent(MouseHelpers.MouseEventFlags.LeftDown);
            MouseHelpers.MouseEvent(MouseHelpers.MouseEventFlags.LeftUp);
            MouseHelpers.SetCursorPosition(x, y);
        }

        private void btnDevtools_Click(object sender, EventArgs e)
        {
            // game.PreparePlayers();
            //Song song = YouTubeClient.GetYTVideoDetails("https://www.youtube.com/watch?v=MFL2bS0jgHM",
            //    Properties.Settings.Default.YtApiKey);
            //Viewer viewer = ChampionshipContext.ContextInstance.Viewers.First(v => v.display == "Vavtrudnir");
            //song.Viewer = viewer;
            //game.SubmittedSongs.Add(song);
            Battle b = game.CurrentRound.Battles.FirstOrDefault(b => b.Position == 8);
            Song s = ChampionshipContext.ContextInstance.Songs.First(s => s.Code == "MFL2bS0jgHM");
            b.Song2 = s;
            //           browserLeft.ShowDevTools();
        }

        public void RefreshBoth()
        {
            browserLeft.Reload();
            browserRight.Reload();
        }

        private bool winnerHasBeenFound = false;
        private void btnReload_Click(object sender, EventArgs e)
        {
            if(winnerHasBeenFound && winnerSong != null)
                GameOnRoundWinnerFound(game, winnerSong);
            else
                RefreshBoth();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(frmChampionship));

        private void btnPlayLeft_Click(object sender, EventArgs e)
        {
            ClickLeft();

        }

        private void ClickLeft()
        {
            MousePoint p = new MousePoint(482, 280);
            //MousePoint p = new MousePoint(leftPlayerCenterX, leftPlayerCenterY);
            PerformMouseClick(p);
            LogToTextbox("Left Click");
        }

        private Timer timer;
        private int playCount;
        private bool isPlayer1Active;



        private void btnPreview_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = Convert.ToInt32(numTotalPreviewTime.Value / 4) * 1000; // 30 seconds
            timer.Tick += OnTimedEvent;
            isPlayer1Active = true;
            playCount = 1;
            ClickLeft();
            timer.Start();
            watch = new();
            watch.Start();
            btnCancelPreview.Enabled = true;
            btnPreview.Enabled = btnVoting.Enabled = false;
            LogToTextbox($"{watch.Elapsed}: Starting Preview");

        }

        private void btnVoting_Click(object sender, EventArgs e)
        {
            obs.SetText(Properties.Settings.Default.LeftVotesSource, "0");
            obs.SetText(Properties.Settings.Default.RightVotesSource, "0");
            game.CurrentRound.CurrentBattle.Votes1 = 
            game.CurrentRound.CurrentBattle.Votes2 = 
            game.CurrentRound.CurrentBattle.Song1.Votes = 
            game.CurrentRound.CurrentBattle.Song2.Votes = 0;
            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
            game.CurrentRound.CurrentBattle.Voted.Clear();
            timer = new Timer();
            timer.Interval = Convert.ToInt32(numTotalVotingTime.Value / 4) * 1000; // 30 seconds
            timer.Tick += OnTimedEvent;
            isPlayer1Active = true;
            playCount = 1;
            ClickLeft();
            timer.Start();
            watch = new();
            watch.Start();
            btnCancelVote.Enabled = true;
            btnVoting.Enabled = btnPreview.Enabled = false;
            game.IsVotingOpen = true;
            obs.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.CountdownSource);
            LogToTextbox($"{watch.Elapsed}: Starting Voting");
            bot.SendMessage("Voting is open!");

        }

        private void btnCancelVote_Click(object sender, EventArgs e)
        {
            if (isPlayer1Active)
                ClickLeft();
            else ClickRight();
            obs.HideItem(Properties.Settings.Default.ChampionshipScene,
                Properties.Settings.Default.CountdownSource);
            game.IsVotingOpen = false;
            CloseVoting();
            
        }

        private Stopwatch watch;
        private void OnTimedEvent(Object source, EventArgs e)
        {

            //double leftPlayerTime = GetPlayerTime(Players.Left);
            //string[] leftPlayerTimeParts = leftPlayerTime.ToString().Split('.');
            //TimeSpan leftPlayerTimeSpan = new TimeSpan(0, 0, 0, int.Parse(leftPlayerTimeParts[0]), int.Parse(leftPlayerTimeParts[1]));

            //double rightPlayerTime = GetPlayerTime(Players.Right);
            //string[] rightPlayerTimeParts = rightPlayerTime.ToString().Split('.');
            //TimeSpan rightPlayerTimeSpan = new TimeSpan(0, 0, 0, int.Parse(rightPlayerTimeParts[0]), int.Parse(rightPlayerTimeParts[1]));

            LogToTextbox(playCount.ToString());
            if (playCount < 4) // Each player plays 2x30sec
            {
                if (isPlayer1Active)
                {
                    LogToTextbox($"{watch.Elapsed}: Stopping left, starting right");
                    ClickLeft();
                    ClickRight();
                    isPlayer1Active = false;

                }
                else
                {
                    LogToTextbox($"{watch.Elapsed}: Stopping right, starting left"); 
                    ClickRight();
                    ClickLeft();
                    isPlayer1Active = true;
                }

                playCount++;
                if (playCount == 4 && game.IsVotingOpen)
                {
                    Timer secTimer = new Timer();
                    secTimer.Interval = 5000;
                    secTimer.Tick += (sender, args) =>
                    {
                        bot.SendMessage("10 seconds left to vote!");
                        secTimer.Stop();
                        secTimer = null;
                        LogToTextbox($"{watch.Elapsed}: Sent countdown message");
                    };
                    secTimer.Start();
                    LogToTextbox($"{watch.Elapsed}: Started countdown message timer");
                }
            }
            else
            {
                LogToTextbox($"{watch.Elapsed}: Time is up.");
                if (isPlayer1Active)
                {
                    ClickLeft();
                    LogToTextbox($"{watch.Elapsed}: Stopping left");
                }
                else
                {
                    ClickRight();
                    LogToTextbox($"{watch.Elapsed}: Stopping right");
                }
                CloseVoting();
            }
        }

        private void CloseVoting()
        {
            if (game.IsVotingOpen)
            {
                LogToTextbox($"{watch.Elapsed}: Voting is open, so close it");
                obs.HideItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.CountdownSource);
                Console.WriteLine("Both players have completed their play time.");

                if (game.CurrentRound.CurrentBattle.Votes1 == game.CurrentRound.CurrentBattle.Votes2)
                {
                    bot.SendMessage("It's a tie!");
                    LogToTextbox($"{watch.Elapsed}: It's a tie");

                }
                else
                {
                    LogToTextbox($"{watch.Elapsed}: Votes are different, find winner");



                }
                game.CloseVoting();
                bot.SendMessage("Voting closed!");
                
            }
            timer.Stop();
            btnCancelPreview.Enabled = false;
            btnCancelVote.Enabled = false;
            btnVoting.Enabled = true;
            btnPreview.Enabled = true;
            timer = null;
            isPlayer1Active = false;
            LogToTextbox($"{watch.Elapsed}: CloseVoting() End: Timer nulled");
            watch.Stop();
            watch = null;
            

        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer?.Stop();
        }

        private void btnCancelPreview_Click(object sender, EventArgs e)
        {
            btnCancelPreview.Enabled = false;
            btnPreview.Enabled = btnVoting.Enabled = true;
            TogglePlayButtons(true);
            if (isPlayer1Active)
                ClickLeft();
            else ClickRight();
            timer.Stop();
            timer = null;
        }

        private Timer recordTimer;

        private void btnRecordPlayerLocations_Click(object sender, EventArgs e)
        {
            TogglePlayButtons(true);
            return;
            recordTimer = new Timer();
            recordTimer.Interval = 1000;
            recordTimer.Tick += RecordTimerOnTick;
            // btnRecordPlayerLocations.Text = "Recording left";
            recordTimer.Start();
        }

        private int leftPlayerCenterX = 484,
            rightPlayerCenterX = 1459,
            leftPlayerCenterY = 302,
            rightPlayerCenterY = 302;

        private int recordCountdown = 2;
        private bool leftDone = false;

        private void RecordTimerOnTick(object sender, EventArgs e)
        {
            btnRecordPlayerLocations.Text =
                "Recording " + (!leftDone ? "left" : "right") + Environment.NewLine + recordCountdown;

            if (recordCountdown == 0)
            {
                recordCountdown = 2;
                if (!leftDone)
                {
                    leftPlayerCenterX = (int)Cursor.Position.X;
                    leftPlayerCenterY = (int)Cursor.Position.Y;
                    leftDone = true;
                    // btnRecordPlayerLocations.Text = "Recording right " + Environment.NewLine + recordCountdown;
                }
                else
                {
                    rightPlayerCenterX = (int)Cursor.Position.X;
                    rightPlayerCenterY = (int)Cursor.Position.Y;
                    btnRecordPlayerLocations.Text = "Record Players";
                    recordTimer.Stop();
                    TogglePlayButtons(true);
                }
            }
            else
            {
                recordCountdown--;
            }
        }

        private void btnVoteLeft_Click(object sender, EventArgs e)
        {
            Viewer me = ChampionshipContext.ContextInstance.Viewers.Find("206992018");
            game.AddVote("1", me);
            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
            string text = $"{me.display} voted 1";
            LogToTextbox(text);
        }

        private void btnVoteRight_Click(object sender, EventArgs e)
        {
            Viewer me = ChampionshipContext.ContextInstance.Viewers.Find("206992018");
            game.AddVote("2", me);
            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
            string text = $"{me.display} voted 2";
            LogToTextbox(text);
        }

        private void btnNextRound_Click(object sender, EventArgs e)
        {
            game.IsVotingOpen = winnerHasBeenFound = false;
            winnerSong = null;
            game.NextBattle();
            RefreshBoth();
            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
        }

        private void btnLeftWins_Click(object sender, EventArgs e)
        {
            game.CurrentRound.CurrentBattle.SetWinner(1);

            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
        }

        private void btnRightWins_Click(object sender, EventArgs e)
        {
            game.CurrentRound.CurrentBattle.SetWinner(2);

            GridControl1.DataSource = game.SubmittedSongs;
            GridControl1.RefreshDataSource();
            GridControl1.Invalidate();
        }

        private void gridView1_CustomDrawCell(object sender,
            DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            //Song row = (Song)gridView1.GetRow(e.RowHandle);
            //if (e.Column == colOut && row.isOut)
            //    e.Appearance.BackColor = Color.Red;


        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0 && view != null)
            {
                Song song = ((Song)view.GetRow(e.RowHandle));
                if (song != null)
                    if (song.isOut)
                        e.Appearance.BackColor = Color.Red;
                    else if (song.won)
                        e.Appearance.BackColor = Color.Green;
                    else if (song.Id == game.CurrentRound?.CurrentBattle?.Song2?.Id || song.Id == game.CurrentRound?.CurrentBattle?.Song1?.Id)
                        e.Appearance.BackColor = Color.Yellow;
                    else
                        e.Appearance.BackColor = Color.White;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Song song = (Song)gridView1.GetFocusedRow();
            if (gridView1 != null && gridView1.GetFocusedValue() != null &&
                gridView1.GetFocusedValue().GetType() == typeof(Uri))
            {
                string url = gridView1.GetFocusedValue().ToString();
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (song != null)
            {
                Song songToEdit = game.SubmittedSongs.First(s => s.Code == song?.Code);
                using (frmAddStarttime frm = new frmAddStarttime(song, game.MinSongLength, game.MaxSongLength))
                {
                    frm.ShowDialog();
                    if (frm.SongChanged)
                    {
                       game.PreparePlayers();
                       RefreshBoth();   
                    }

                }
            }
        }

        internal enum Players
        {
            Left,
            Right
        }
    }
}

