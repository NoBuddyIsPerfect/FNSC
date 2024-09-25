using EmbedIO.Actions;
using EmbedIO;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmbedIO.WebApi;
using EmbedIO.Files;
using System.Threading.Tasks;
using System.Security.Policy;
using FNSC.Externals;
using FNSC.Data;
using DevExpress.LookAndFeel.Design;
using FNSC.Data;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using FNSC.Classes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using FNSC.Integrations.Classes;
using Microsoft.EntityFrameworkCore;
using FNSC.Data.StreamerBotClasses;
using FNSC.Externals;
using FNSC.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using log4net;


namespace FNSC
{
    public partial class frmManagement : DevExpress.XtraEditors.XtraForm
    {
        private static readonly StreamerbotClient streamerbotClient = new();
        private static readonly ObsClient obsClient = new();
        private static volatile SubmissionQueue _submissionQueueInstance;
        private static readonly object _mutex = new();
        private BindingList<Song> queueList;
        private bool queueOpen = false;
        private static readonly ILog log = LogManager.GetLogger(typeof(frmManagement));

        public SubmissionQueue SubmissionQueue
        {
            get
            {
                if (_submissionQueueInstance == null)
                {
                    lock (_mutex)
                    {
                        _submissionQueueInstance = new SubmissionQueue();


                    }
                }
                return _submissionQueueInstance;
            }
        }

        private bool isDebug = false;
        public frmManagement(bool enableDebug = false)
        {
            InitializeComponent();
            obsClient.Connected += ObsClient_Connected;
            streamerbotClient.CommandReceived += StreamerbotClient_CommandReceived;
            streamerbotClient.ChatMessageReceived += StreamerbotClient_ChatMessageReceived;
            isDebug = enableDebug;
            ToggleDebugFeatures();
            RefreshUserComboBox();

        }

        private void ToggleDebugFeatures()
        {
            btnSaveGame.Visible = btnLoadGame.Visible = cboGames.Visible = btnDummyData.Visible = isDebug;
        }

        private void GameOnVotesChanged(object sender, VotesChangedEventArgs e)
        {
            obsClient.SetText(Properties.Settings.Default.LeftVotesSource, e.Votes1.ToString());
            obsClient.SetText(Properties.Settings.Default.RightVotesSource, e.Votes2.ToString());

        }

        private void StreamerbotClient_ChatMessageReceived(object sender, ChatMessageEventArgs e)
        {
            StreamerBotChatMessagePart chatMessagePart = e.MessagePart;
            Viewer viewer = Viewer.Convert(chatMessagePart.Message.userId, chatMessagePart.Message.username,
                chatMessagePart.Message.displayName, chatMessagePart.Message.subscriber, chatMessagePart.Message.role,
                "twitch");
            if (ChampionshipContext.ContextInstance.Viewers.Find(viewer.id) == null)
            {
                ChampionshipContext.ContextInstance.Add(viewer);
                ChampionshipContext.ContextInstance.SaveChanges();
            }

            comboSubmissionViewer.Invoke(new RefreshUserComboBoxDelegate(RefreshUserComboBox));
        }

        private void RefreshUserComboBox()
        {
            comboSubmissionViewer.DataSource = ChampionshipContext.ContextInstance.Viewers.OrderBy(v => v.display).ToList();
            comboSubmissionViewer.DisplayMember = "display";
            comboSubmissionViewer.ValueMember = "id";
        }
        private void RefreshGameComboBox(Game game1 = null)
        {
            ChampionshipContext.ContextInstance.Games
                .Include(game => game.PreSubmittedSongs)
                .ThenInclude(song => song.Viewer)
                .Include(game => game.Rounds)
                .ThenInclude(round => round.Battles)
                .ThenInclude(battle => battle.Song1)
                .ThenInclude(song => song.Viewer)
                .Include(game => game.CurrentRound.Battles)

                .Include(game => game.CurrentRound.CurrentBattle)
                .Include(game => game.CurrentRound.FinishedBattles)

                .ToList();
            cboGames.Properties.DataSource = ChampionshipContext.ContextInstance.Games.ToList();
            cboGames.Properties.DisplayMember = "ComboBoxText";
            cboGames.Properties.KeyMember = "Id";
            if (game1 != null)
                cboGames.EditValue = game1;

        }

        private void StreamerbotClient_CommandReceived(object sender, FNSC.Integrations.Classes.CommandEventArgs e)
        {
            if (e.Command.command == "youtube.com" || e.Command.command == "youtu.be")
            {
                try
                {
                    Song submitttedSong =
                        YouTubeClient.GetYTVideoDetails(e.Command.message, Properties.Settings.Default.YtApiKey);
                    submitttedSong.Viewer = Viewer.Convert(e.Command.user);
                    AddSongToQueue(submitttedSong);
                }
                catch (Exception ex)
                {

                }
            }
            else if (e.Command.command == "1" || e.Command.command == "2")
                if (game.AddVote(e.Command.command, Viewer.Convert(e.Command.user)) && game.SendWhispers)
                    streamerbotClient.SendWhisper(e.Command.user.display, "Vote counted!");


        }

        private bool AddSongToQueue(Song submitttedSong)
        {
            if (queueOpen && game != null)
            {
                if (submitttedSong.Description == "NO TITLE" && game.SendWhispers)
                {
                    streamerbotClient.SendWhisper(submitttedSong.Viewer.display, "Sorry, I could not retrieve your Song from YT!");
                    return false;
                }
                if (SubmissionQueue.Count(s => s.Viewer.id == submitttedSong.Viewer.id) >= game?.NoOfSongsPerPerson && game.SendWhispers)
                {
                    streamerbotClient.SendWhisper(submitttedSong.Viewer.display,
                        "Sorry, " + submitttedSong.Viewer.display +
                        ", you have already submitted the allowed number of songs!");
                    return false;
                }
                if (SubmissionQueue.Count(s => s.Code == submitttedSong.Code) > 0 && !game.AllowDoubles && game.SendWhispers)
                {
                    streamerbotClient.SendWhisper(submitttedSong.Viewer.display,
                        "Sorry, " + submitttedSong.Viewer.display + ", that Song has already been submitted!");
                    return false;
                }

                if (!queueOpen && game.SendWhispers)
                {
                    streamerbotClient.SendWhisper(submitttedSong.Viewer.display, "Sorry, Your Song could not be added because submissions are closed!");

                    return false;

                }
                if (submitttedSong.IsBlocked && game.SendWhispers)
                {

                    streamerbotClient.SendWhisper(submitttedSong.Viewer.display, "Sorry, that video is either age restricted or blocked in Germany! Please submit a different video!");
                    return false;
                }
                if (submitttedSong.Length < new TimeSpan(0, 2, 30))
                {
                    streamerbotClient.SendMessage("Sorry, @" + submitttedSong.Viewer.display + ", that Song is too short. Please make sure your Song is at least 2:30 min long.");
                    return true;
                }
                if (submitttedSong.Length.Subtract(new TimeSpan(0, 0, submitttedSong.InitialStarttime)) < game.MinSongLength)
                {
                    streamerbotClient.SendMessage("Sorry, @" + submitttedSong.Viewer.display + ", that Song is too short. Please make sure your Song has at least 2:30 min of playtime after the start time!");
                    return true;
                }
                if (game.MaxSongLength.TotalMilliseconds > 0 && game.MaxSongLength < submitttedSong.Length && game.SendWhispers)
                {

                    streamerbotClient.SendWhisper(submitttedSong.Viewer.display, "Sorry, Your Song could not be added because it is too long!");
                    return false;
                }
                {
                    ChampionshipContext.ContextInstance.Add(submitttedSong);
                    SubmissionQueue.Enqueue(submitttedSong);
                    game.PreSubmittedSongs.Add(submitttedSong);
                    ChampionshipContext.ContextInstance.SaveChanges();
                    if (SubmissionQueue.Count == game.NoOfSongs)
                    {
                        CloseSubmissions();
                    }
                    else
                    {
                        string text = "Theme for championship no " + this.game.ChampionshipNumber + ":\n\r\n" +
                                      this.game.Theme + "\n\r\n\rSubmissions are open\n\r\nSongs: " +
                                      this.SubmissionQueue.Count + "/" + this.game.NoOfSongs + "\n\r\nSongs/person: " +
                                      this.game.NoOfSongsPerPerson;
                        obsClient.SetText(Properties.Settings.Default.MainTextSource, text);
                        // streamerbotClient.SendMessage("Sorry, " + submitttedSong.Viewer.display + ", you have already submitted the allowed number of songs!");
                    }
                    if (game.SendWhispers)
                        streamerbotClient.SendWhisper(submitttedSong.Viewer.display,
                        $"You Song {submitttedSong.Description} has been submitted!");

                    return true;
                }
            }
            else
                return false;
        }

        private void CloseSubmissions()
        {
            queueOpen = false;
            if (btnCloseSubmissions.InvokeRequired)
                btnCloseSubmissions.Invoke(() => { btnCloseSubmissions.Enabled = false; });
            else
                btnCloseSubmissions.Enabled = false;
            if (btnOpenSubmissions.InvokeRequired)
                btnOpenSubmissions.Invoke(() => { btnOpenSubmissions.Enabled = true; });
            else
                btnOpenSubmissions.Enabled = true;

            obsClient.SetText(Properties.Settings.Default.MainTextSource, "Submissions are closed!");
        }

        private void ObsClient_Connected(object sender, EventArgs e)
        {
            // obsClient.GetSceneItems();

        }
        private Game game;

        private void ConnectStreamerBot(object? obj)
        {
            if (streamerbotClient.Connect(Properties.Settings.Default.StreamerBotIp, Properties.Settings.Default.StreamerBotPort, false))
            {
                streamerbotClient.GetActions();

                //streamerbotClient.ExecuteAction("537f127a-5066-4166-a32d-b57e15fb5786", "NBSC TEST", new Dictionary<string, string>() { { "rawInput", "Kommt vom Programm" } });

            }
            else
            {

            }
        }

        private CancellationTokenSource cts;
        private void Form1_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();

            //streamerbotClient.ActionsReceived += StreamerbotClient_ActionsReceived;
            //Thread submissionThread = new Thread(ConnectStreamerBot);
            //submissionThread.Start();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectStreamerBot), cts.Token);
            //ChampionshipContext championshipContext = new ChampionshipContext();
            //Game? game = championshipContext.FindAsync<Game>(1).Result;
            bool ObsConnectSuccess = obsClient.Connect($"ws://{Properties.Settings.Default.ObsIp}:{Properties.Settings.Default.ObsPort}", Properties.Settings.Default.ObsPassword);
            RefreshGameComboBox();

        }

        private void InitChampionship()
        {
            game = Game.Init(txtTheme.Text, Convert.ToInt32(numNoOfSongs.Value),
                Convert.ToInt32(numSongsPerPerson.Value), Convert.ToInt32(numChampionshipNumber.Value),
                Convert.ToInt32(numPreviewTime.Value), Convert.ToInt32(numVotingTime.Value), txtMinLength.Text, txtMaxLength.Text, chkWhispers.Checked,
                chkAllowDoubles.Checked, chkAllowVoteCorrection.Checked);
            SubmissionQueue.Clear();
            SubmissionQueue.SongEnqueued += SubmissionQueueOnSongEnqueued;
            SubmissionQueue.SongDequeued += SubmissionQueueOnSongDequeued;
            queueList = new BindingList<Song>();
            gridControl1.DataSource = queueList;

            game.VotesChanged += GameOnVotesChanged;
            //    obsClient.SetText(Properties.Settings.Default.MainTextSource, text);

        }


        private void SubmissionQueueOnSongDequeued(object sender, SongQueueEventArgs e)
        {
            gridControl1.Invoke(new RemoveSongToSubmissionQueueDelegate(RemoveSongFromSubmissionQueue), e.Song);

        }
        private delegate void AddSongToSubmissionQueueDelegate(Song song);
        private delegate void RemoveSongToSubmissionQueueDelegate(Song song);
        private delegate void RefreshUserComboBoxDelegate();

        private void SubmissionQueueOnSongEnqueued(object sender, SongQueueEventArgs e)
        {
            gridControl1.Invoke(new AddSongToSubmissionQueueDelegate(AddSongToSubmissionQueue), e.Song);


        }
        private void AddSongToSubmissionQueue(Song song)
        {
            if (!queueList.Contains(song))
            {
                queueList.Add(song);

            }
        }
        private void RemoveSongFromSubmissionQueue(Song song)
        {
            if (queueList.Contains(song))
            {
                queueList.Remove(song);

            }
        }
        private void btnStartChampionship_Click(object sender, EventArgs e)
        {
            queueOpen = false;
            game.SubmissionsOpen = true;
            while (SubmissionQueue.TryDequeue(out Song song))
            {
                game.SubmitSong(song);
                if (!game.SubmissionsOpen)
                    break;
            }
            game.StartChampionship();
            btnSaveGame_Click(null, null);
            obsClient.SetText(Properties.Settings.Default.LeftVotesSource, "0");
            obsClient.SetText(Properties.Settings.Default.RightVotesSource, "0");
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.MainTextSource);
            obsClient.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.AllVotesSource);
            obsClient.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RightPlayerSource);
            obsClient.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.LeftPlayerSource);
            obsClient.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.VsSource);
            
            obsClient.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RoundHeaderSource);

            frmChampionship frm = new frmChampionship(game, obsClient, streamerbotClient);
            frm.Show();
        }


        private void btnOpenSubmissions_Click(object sender, EventArgs e)
        {
            OpenSubmissions();

        }

        private void OpenSubmissions()
        {
            btnCloseSubmissions.Enabled = queueOpen = true;
            btnOpenSubmissions.Enabled = !queueOpen;
            string text = "Theme for championship no " + this.game.ChampionshipNumber + ":\n\r\n" + this.game.Theme + "\n\r\n\rSubmissions are open\n\r\nSongs: " + this.SubmissionQueue.Count + "/" + this.game.NoOfSongs + "\n\r\nSongs/person: " + this.game.NoOfSongsPerPerson;
            obsClient.SetText(Properties.Settings.Default.MainTextSource, text);
            obsClient.ShowItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.MainTextSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.AllVotesSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RightPlayerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.LeftPlayerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.VsSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RoundHeaderSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.CountdownSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerBottomSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerTopSource);


        }

        private void btnCloseSubmissions_Click(object sender, EventArgs e)
        {
            CloseSubmissions();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.MenuType == GridMenuType.Row)
            {
                int rowHandle = e.HitInfo.RowHandle;
                // Deletes existing menu items, if any.
                e.Menu.Items.Clear();
                // Adds the 'Cell Merging' check item to the context menu.
                DXMenuItem item = CreateMenuItemEditSubmission(view, rowHandle);
                e.Menu.Items.Add(item);
            }
        }

        DXMenuItem CreateMenuItemEditSubmission(GridView view, int rowHandle)
        {
            DXMenuItem checkItem = new DXMenuItem("&Edit", new EventHandler(OnSubmissionEditClick));
            checkItem.Tag = new RowInfo(view, rowHandle);
            return checkItem;
        }
        void OnSubmissionEditClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            RowInfo info = item?.Tag as RowInfo;
            Song song = (Song)info?.View.GetRow(info.RowHandle);
            Song song2 = queueList.First(s => s.Code == song?.Code);
            using (frmAddStarttime frm = new frmAddStarttime(song, game.MinSongLength, game.MaxSongLength))
            {
                frm.ShowDialog();


            }
        }

        class RowInfo
        {
            public RowInfo(GridView view, int rowHandle)
            {
                RowHandle = rowHandle;
                View = view;
            }
            public GridView View;
            public int RowHandle;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Song song = (Song)gridView1.GetFocusedRow();
                if (song != null)
                {
                    queueList.Remove(song);
                    SubmissionQueue.Remove(song);
                }
                if (queueList.Count < game.NoOfSongs)
                    OpenSubmissions();
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
                Song songToEdit = queueList.First(s => s.Code == song?.Code);
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (comboSubmissionViewer.SelectedValue != null)
            {
                Song submitttedSong =
                    YouTubeClient.GetYTVideoDetails(txtSubmissionUrl.Text, Properties.Settings.Default.YtApiKey);
                submitttedSong.Viewer =
                    ChampionshipContext.ContextInstance.Viewers.Find(comboSubmissionViewer.SelectedValue.ToString());
                AddSongToQueue(submitttedSong);
                txtSubmissionUrl.Text = String.Empty;

            }
            //Thread submissionThread = new Thread(ConnectStreamerBot);
            //submissionThread.Start();
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {




            //    ChampionshipContext.ContextInstance.Games.Update(game);
            //ChampionshipContext.ContextInstance.SaveChanges();
            //RefreshGameComboBox(game);
        }

        private void btnDummyData_Click(object sender, EventArgs e)
        {
            bool success = SubmitSong("https://www.youtube.com/watch?v=5NV6Rdv1a3I&t=10", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=TdrL3QxjyVw&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=iPUmE-tne5U&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=sRjm5ZGNMLQ&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=apBWI6xrbLY&t=22", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=NdYWuo9OFAw&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=nYJShuA9F84&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=Ho1LgF8ys-c&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=aSkFygPCTwE&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=3KnHgNT7Dd4&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=_M-DXPT04_Y&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=E0Kv6vxZwL8&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=1UvphggoEag&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=Dud63tz5JPU&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=32Oc2d_3yEk&t=0", "876112028");
            if (success)
                Thread.Sleep(500);
            if (success)
                success = SubmitSong("https://www.youtube.com/watch?v=0FWRT9C9XMQ&t=0", "876112028");

        }

        private bool SubmitSong(string url, string userId)
        {
            Song submitttedSong =
                YouTubeClient.GetYTVideoDetails(url, Properties.Settings.Default.YtApiKey);
            submitttedSong.Viewer =
                ChampionshipContext.ContextInstance.Viewers.FirstOrDefault(v => v.id == userId);
            return AddSongToQueue(submitttedSong);
        }

        private void BtnResetChampionshipClick(object sender, EventArgs e)
        {
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.MainTextSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.AllVotesSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RightPlayerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.LeftPlayerSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.VsSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.RoundHeaderSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerBottomSource);
            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.CountdownSource);

            obsClient.HideItem(Properties.Settings.Default.ChampionshipScene, Properties.Settings.Default.WinnerTopSource);
            btnInitChampionship.Enabled = true;
            btnStartChampionship.Enabled = btnModifyChampionship.Enabled = btnResetChampionship.Enabled = false;
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            StringBuilder messageBoxCS = new StringBuilder();
            messageBoxCS.AppendFormat("Button = {0}", e.Button);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("Clicks = {0}", e.Clicks);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("X = {0}", e.X);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("Y = {0}", e.Y);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("Delta = {0}", e.Delta);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("Location = {0}", e.Location);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("cursor = {0}", System.Windows.Forms.Cursor.Position);
            messageBoxCS.AppendLine();

            //            MessageBox.Show(messageBoxCS.ToString(), "MouseClick Event");
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            if (cboGames.EditValue != null)
                game = ChampionshipContext.ContextInstance.Games.Find(cboGames.EditValue);
            if (game == null)
                return;
            foreach (Round gameRound in game.Rounds)
            {
                ChampionshipContext.ContextInstance.Entry(gameRound).Collection(g => g.Battles).Load();
            }
            ChampionshipContext.ContextInstance.Entry(game).Collection(g => g.SubmittedSongs).Load();
            if (queueList == null)
                queueList = new BindingList<Song>();
            else
                queueList.Clear();
            foreach (Song gameSubmittedSong in game.PreSubmittedSongs)
            {
                queueList.Add(gameSubmittedSong);
                SubmissionQueue.Enqueue(gameSubmittedSong);
            }

            numVotingTime.Value = game.VotingTime;
            numPreviewTime.Value = game.PreviewTime;
            numSongsPerPerson.Value = game.NoOfSongsPerPerson;
            numNoOfSongs.Value = game.NoOfSongs;
            txtTheme.Text = game.Theme;
            btnOpenSubmissions.Enabled = !game.SubmissionsOpen;
            btnCloseSubmissions.Enabled = game.SubmissionsOpen;

            //  SubmissionQueue.Clear();
            SubmissionQueue.SongEnqueued += SubmissionQueueOnSongEnqueued;
            SubmissionQueue.SongDequeued += SubmissionQueueOnSongDequeued;
            gridControl1.DataSource = queueList;

            game.VotesChanged += GameOnVotesChanged;
        }

        private void btnInitChampionship_Click(object sender, EventArgs e)
        {
            InitChampionship();
            RefreshUserComboBox();
            RefreshGameComboBox(ChampionshipContext.ContextInstance.Games.FirstOrDefault());
            OpenSubmissions();
            btnInitChampionship.Enabled = false;
            btnModifyChampionship.Enabled = btnStartChampionship.Enabled = btnResetChampionship.Enabled = true;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            obsClient.Disconnect();
            cts.Cancel();
            this.Hide();
            Thread.Sleep(500);
            cts.Dispose();
        }


        private void btnModifyChampionship_Click(object sender, EventArgs e)
        {
            if (game == null)
                return;
            string[] parts = txtMaxLength.Text.Split(':');
            if (parts.Length == 2)
            {
                int min = int.Parse(parts[0]);
                int sec = int.Parse(parts[1]);
                game.MinSongLength = new TimeSpan(0, min, sec);
            }


            parts = txtMaxLength.Text.Split(':');
            if (parts.Length == 2)
            {
                int min = int.Parse(parts[0]);
                int sec = int.Parse(parts[1]);
                game.MaxSongLength = new TimeSpan(0, min, sec);
            }

            game.PreviewTime = Convert.ToInt32(numPreviewTime.Value);
            game.VotingTime = Convert.ToInt32(numVotingTime.Value);

            game.NoOfSongsPerPerson = Convert.ToInt32(numSongsPerPerson.Value);
            game.AllowDoubles = chkAllowDoubles.Checked;
            game.AllowVoteCorrection = chkAllowVoteCorrection.Checked;
            game.SendWhispers = chkWhispers.Checked;
            game.NoOfSongs = Convert.ToInt32(numNoOfSongs.Value);
            if (SubmissionQueue.Count < game.NoOfSongs)
                OpenSubmissions();
            string text = "Theme for championship no " + this.game.ChampionshipNumber + ":\n\r\n" + this.game.Theme + "\n\r\n\rSubmissions are open\n\r\nSongs: " + this.SubmissionQueue.Count + "/" + this.game.NoOfSongs + "\n\r\nSongs/person: " + this.game.NoOfSongsPerPerson;
            obsClient.SetText(Properties.Settings.Default.MainTextSource, text);

        }

    }
}
