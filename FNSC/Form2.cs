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

namespace FNSC
{
    public partial class Form2 : Form
    {
        public static Form2 Instance { get; private set; }


        int previewTime = 30;
        private readonly int startLeft = 25;
        private readonly int startRight = 0;
        private Game game;
        private ObsClient obs;
        private StreamerbotClient bot;

        public Form2(Game game, ObsClient obsObs, StreamerbotClient bot)
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
            gridControl1.DataSource = game.SubmittedSongs;
            numTotalPreviewTime.Value = game.PreviewTime;
            numTotalVotingTime.Value = game.VotingTime;
            bot.ActionExecuted += BotOnActionExecuted;
            game.VoteReceived += GameOnVoteReceived;
            game.WinnerFound += GameOnWinnerFound;
            game.StartNextBattle += GameOnStartNextBattle;
            Instance = this;
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
            else if ((game.CurrentRound.Battles.Count + game.CurrentRound.FinishedBattles.Count) == 1)
                headerText = "Final";
            obs.SetText(Properties.Settings.Default.RoundHeaderSource, headerText);
        }


        private delegate void AppendLogTextboxDelegate(string message);
        private void GameOnVoteReceived(object sender, VoteReceivedEventArgs e)
        {
            string text = $"{e.Viewer.display} voted {e.Vote}";
            // LogToTextbox(text);
        }
        private void GameOnWinnerFound(object sender, EventArgs e)
        {
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.MainTextSource);
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.AllVotesSource);
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RightPlayerSource);
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.LeftPlayerSource);
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.VsSource);
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RoundHeaderSource);
            //obs.SetText(Properties.Settings.Default.WinnerTopSource, $"{game.CurrentRound.CurrentBattle.Winner.Channel} -\n{(game.CurrentRound.CurrentBattle.Winner.Description.Length < 30 ? game.CurrentRound.CurrentBattle.Winner.Description : game.CurrentRound.CurrentBattle.Winner.Description.Substring(0, 30))}");
            //obs.SetText(Properties.Settings.Default.WinnerBottomSource, $"by {game.CurrentRound.CurrentBattle.Winner.Viewer.display}");
            obs.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerTopSource);
            obs.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerSource);

            obs.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerBottomSource);

        }
        //public void LogToTextbox(string message)
        //{
        //    if (txtLogOutput.InvokeRequired)
        //        txtLogOutput.Invoke(new AppendLogTextboxDelegate(WriteTextToLogControl), message);
        //    else
        //        txtLogOutput.AppendText(Environment.NewLine + message);
        //}
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

        static Form2()
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
            game.PreparePlayers();


 //           browserLeft.ShowDevTools();
        }

        public void RefreshBoth()
        {
            browserLeft.Reload();
            browserRight.Reload();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            RefreshBoth();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(Form2));

        private void btnPlayLeft_Click(object sender, EventArgs e)
        {
            ClickLeft();

        }

        private void ClickLeft()
        {
            MousePoint p = new MousePoint(482, 280);
            //MousePoint p = new MousePoint(leftPlayerCenterX, leftPlayerCenterY);
            PerformMouseClick(p);
        }

        private Timer timer;
        private int playCount;
        private bool isPlayer1Active;


        private void btnPreview_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = Convert.ToInt32(numTotalPreviewTime.Value / 4) * 1000; // 30 seconds
            timer.Tick += OnTimedEvent;
            playCount = 0;
            isPlayer1Active = true;
            playCount = 1;
            ClickLeft();
            timer.Start();
            btnCancelPreview.Enabled = true;
            btnPreview.Enabled = btnVoting.Enabled = false;
        }

        private void btnVoting_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = Convert.ToInt32(numTotalVotingTime.Value / 4) * 1000; // 30 seconds
            timer.Tick += OnTimedEvent;
            playCount = 0;
            isPlayer1Active = true;
            playCount = 1;
            ClickLeft();
            timer.Start();
            btnCancelVote.Enabled = true;
            btnVoting.Enabled = btnPreview.Enabled = false;
            game.IsVotingOpen = true;
            obs.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.CountdownSource);
        }

        private void btnCancelVote_Click(object sender, EventArgs e)
        {
            btnCancelVote.Enabled = false;
            btnVoting.Enabled = btnPreview.Enabled = true;
            TogglePlayButtons(true);
            if (isPlayer1Active)
                ClickLeft();
            else ClickRight();
            timer.Stop();
            game.CloseVoting();
            obs.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.CountdownSource);
            //  game.ResetVoting();
        }

        private void OnTimedEvent(Object source, EventArgs e)
        {

            //double leftPlayerTime = GetPlayerTime(Players.Left);
            //string[] leftPlayerTimeParts = leftPlayerTime.ToString().Split('.');
            //TimeSpan leftPlayerTimeSpan = new TimeSpan(0, 0, 0, int.Parse(leftPlayerTimeParts[0]), int.Parse(leftPlayerTimeParts[1]));

            //double rightPlayerTime = GetPlayerTime(Players.Right);
            //string[] rightPlayerTimeParts = rightPlayerTime.ToString().Split('.');
            //TimeSpan rightPlayerTimeSpan = new TimeSpan(0, 0, 0, int.Parse(rightPlayerTimeParts[0]), int.Parse(rightPlayerTimeParts[1]));

            Console.WriteLine(playCount);
            if (playCount < 4) // Each player plays 2x30sec
            {
                if (isPlayer1Active)
                {
                    ClickLeft();
                    ClickRight();
                    isPlayer1Active = false;

                }
                else
                {
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
                    };
                    secTimer.Start();
                }
            }
            else
            {
                if (isPlayer1Active)
                    ClickLeft();
                else
                    ClickRight();
                CloseVoting();
            }
        }

        private void CloseVoting()
        {
            if (game.IsVotingOpen)
            {
                obs.HideItem(Properties.Settings.Default.ChampionshipScene,
                    Properties.Settings.Default.CountdownSource);
                timer.Stop();
                Console.WriteLine("Both players have completed their play time.");

                if (game.CurrentRound.CurrentBattle.Votes1 == game.CurrentRound.CurrentBattle.Votes2)
                    bot.SendMessage("It's a tie!");
                else
                    game.CloseVoting();
            }
            btnCancelPreview.Enabled = false;
            btnCancelVote.Enabled = false;
            btnVoting.Enabled = true;
            btnPreview.Enabled = true;

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
            game.AddVote("1", ChampionshipContext.ContextInstance.Viewers.Find("876112028"));
        }

        private void btnVoteRight_Click(object sender, EventArgs e)
        {
            game.AddVote("2", ChampionshipContext.ContextInstance.Viewers.Find("876112028"));
        }

        private void btnNextRound_Click(object sender, EventArgs e)
        {
            game.NextBattle();
            RefreshBoth();
            gridControl1.DataSource = game.SubmittedSongs;
            gridControl1.RefreshDataSource();
            gridControl1.Invalidate();
        }

        private void btnLeftWins_Click(object sender, EventArgs e)
        {
            game.CurrentRound.CurrentBattle.SetWinner(1);

            gridControl1.DataSource = game.SubmittedSongs;
            gridControl1.RefreshDataSource();
            gridControl1.Invalidate();
        }

        private void btnRightWins_Click(object sender, EventArgs e)
        {
            game.CurrentRound.CurrentBattle.SetWinner(2);

            gridControl1.DataSource = game.SubmittedSongs;
            gridControl1.RefreshDataSource();
            gridControl1.Invalidate();
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
                    else if (song.Id == game.CurrentRound.CurrentBattle.Song2.Id || song.Id == game.CurrentRound.CurrentBattle.Song1.Id)
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
                    //if (frm.SongChanged)
                    //{
                    //    queueList.Remove(songToEdit);
                    //    SubmissionQueue.Remove(songToEdit);
                    //    queueList.Add(frm.Song);
                    //    SubmissionQueue.Add(frm.Song);
                    //}

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

