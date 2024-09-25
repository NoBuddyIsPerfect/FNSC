using FNSC.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Data.Helpers;
using System.ComponentModel;
using System.IO;
using DevExpress.XtraRichEdit.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FNSC.Classes;
using log4net;

namespace FNSC.Data
{

    public class VotesChangedEventArgs
    {
        public int Votes1;
        public int Votes2;

        public VotesChangedEventArgs(int votes1, int votes2)
        {
            Votes1 = votes1;
            Votes2 = votes2;
        }
    }
    public class VoteReceivedEventArgs
    {
        public int Vote;
        public Viewer Viewer;

        public VoteReceivedEventArgs(int vote, Viewer viewer)
        {
            Vote = vote;
            Viewer = viewer;
        }
    }
    public class Game
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Game));

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int NoOfRounds { get; set; }
        public ICollection<Round> Rounds { get; set; } = new List<Round>();
        public int PreviewTime { get; set; } = 120;
        public int VotingTime { get; set; } = 60;
        public bool IsVotingOpen { get; set; }
        public bool AllowVoteCorrection { get; set; } = false;
        public int NoOfSongsPerPerson { get; set; } = 2;
        public int NoOfSongs { get; set; }
        public int ChampionshipNumber { get; set; } = 1;
        public string Theme { get; set; } = "No theme";
        public ICollection<Song> SubmittedSongs { get; set; } = new BindingList<Song>();
        public ICollection<Song> PreSubmittedSongs { get; set; } = new List<Song>();
        public bool SubmissionsOpen { get; set;  } = false;
        public bool GameFinished { get; set;  } = false;
        public bool AllowDoubles { get; set; } = false;

        [NotMapped]
        public bool SendWhispers { get; set; }
        public TimeSpan MaxSongLength { get; set; } = new TimeSpan(0, 0, 0);
        public TimeSpan MinSongLength { get; set; } = new TimeSpan(0, 2, 30);

        public DateTime InitTimestamp { get; set; }
        public DateTime FinishTimestamp { get; set; }

        public event EventHandler<VotesChangedEventArgs> VotesChanged;
        public event EventHandler<VoteReceivedEventArgs> VoteReceived;
        public event EventHandler<EventArgs> WinnerFound;
        public event EventHandler<EventArgs> StartNextBattle;
        [NotMapped]
        [Display(ShortName = "Text")]

        public string ComboBoxText
        {
            get
            {
                return $"{ChampionshipNumber} - {Theme}";
            }
        }

        public Game() { }

        public Round CurrentRound { get; set; }
    
        public void SubmitSong(Song song)
        {
           if(SubmittedSongs.Count(s => s.Viewer == song.Viewer) < NoOfSongsPerPerson)
            SubmittedSongs.Add(song);
           if (SubmittedSongs.Count == NoOfSongs)
               SubmissionsOpen = false;

           
        }


        private void OnVotesChanged(VotesChangedEventArgs args)
        {

            EventHandler<VotesChangedEventArgs> handler = VotesChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }
      
        private void OnVoteReceived(VoteReceivedEventArgs args)
        {

            EventHandler<VoteReceivedEventArgs> handler = VoteReceived;

            if (handler != null)
            {
                handler(this, args);
            }
        }
      

        private void OnNextBattle(EventArgs args)
        {

            EventHandler<EventArgs> handler = StartNextBattle;

            if (handler != null)
            {
                handler(this, args);
            }
        }
        private void OnWinnerFound(EventArgs args)
        {

            EventHandler<EventArgs> handler = WinnerFound;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public bool AddVote(string vote, Viewer viewer)
        {
            Battle currBattle = CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo);

            if (!IsVotingOpen || currBattle == null)
                return false;
            else
            {
                bool hasVoted = currBattle.Voted.Keys.Contains(viewer);

                if (hasVoted && !AllowVoteCorrection)
                    return false;

                if (hasVoted && AllowVoteCorrection)
                {
                    string oldVote = currBattle.Voted[viewer];
                    if (oldVote != vote)
                    {
                        currBattle.Voted[viewer] = vote;
                        if (oldVote == "1")
                            currBattle.Votes1--;
                        else
                            currBattle.Votes2--;

                        if (vote == "1")
                            currBattle.Votes1++;
                        else
                            currBattle.Votes2++;

                    }
                }else if (!hasVoted)
                {
                    if (vote == "1")
                        currBattle.Votes1++;
                    else
                        currBattle.Votes2++;

                }

                if (!hasVoted)
                    currBattle.Voted.Add(viewer, vote);
                OnVotesChanged(new VotesChangedEventArgs(currBattle.Votes1, currBattle.Votes2));
                OnVoteReceived(new VoteReceivedEventArgs(int.Parse(vote), viewer));
            }

            return true;
        }
        public static Game Init(string theme, int noOfSongs)
        {
            Game newGame = new Game();
            newGame.Rounds.Clear();
            newGame.NoOfRounds = GameHelper.CalculateNoOfRounds(noOfSongs);
            newGame.NoOfSongs = noOfSongs;
            for (int i = 0; i < newGame.NoOfRounds; i++)
                newGame.Rounds.Add(new Round(i+1));
            newGame.SubmittedSongs = new BindingList<Song>();
            newGame.PreSubmittedSongs = new List<Song>();
            newGame.InitTimestamp = DateTime.Now;
            
            return newGame;
        }

        public void StartChampionship()
        {
           PrepareRound();
        }

        public void PrepareRound()
        {
            Round round1 = Rounds.FirstOrDefault(r => r.RoundNumber == (CurrentRound == null? 1:CurrentRound.RoundNumber+1));
            if (round1 != null)
            {
                round1.Battles = GameHelper.GroupSongsInRounds((CurrentRound != null ? CurrentRound.FinishedBattles.Select(b => b.Winner).ToList(): SubmittedSongs.ToList()));
                foreach (Battle battle in round1.Battles)
                {
                    battle.Round = round1;
                    battle.Song1.won = battle.Song1.isOut = false;
                    battle.Song2.won = battle.Song2.isOut = false;
                }
                round1.CurrentBattle = round1.Battles.FirstOrDefault(b => b.Position == 1);
                CurrentRound = round1;

            }
            else
            {
                PrepareWinner();
            }
          
            
        }

        public void NextBattle()
        {
            Battle currBattle = CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo);
            if (!IsVotingOpen && currBattle != null && currBattle.Winner != null && CurrentRound != null)
            {
                CurrentRound.CurrentBattleNo++;
                CurrentRound.FinishedBattles.Add(currBattle);
                CurrentRound.Battles.Remove(currBattle);
                Battle nextBattle = CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo);
                if (nextBattle == null)
                {
                    PrepareRound();
                    nextBattle = CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo);

                }

                CurrentRound.CurrentBattle = nextBattle;
                PreparePlayers();
                OnVotesChanged(new VotesChangedEventArgs(0,0));
                OnNextBattle(new ());
            }
        }

        public void CloseVoting()
        {
            CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.CloseVoting();
            IsVotingOpen = false;
        }

        public void ResetVoting()
        {
            Battle currBattle = CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo);
            if (currBattle != null)
            {
                currBattle.ResetVoting();
                OnVotesChanged(new VotesChangedEventArgs(currBattle.Votes1, currBattle.Votes2));
            }
        }

        public void PreparePlayers()
        {
            if (CurrentRound != null)
            {
                string path = Properties.Settings.Default.WebserverPath;
                string tmpl = File.ReadAllText(Path.Combine(path, "player.tmpl"));
                string left = tmpl.Replace("<CODE>",CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.Song1.Code)
                    .Replace("<START>", CurrentRound.RoundNumber == 1 ? CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.Song1.InitialStarttime.ToString():CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.Song1.Starttime.ToString());
                string right = tmpl.Replace("<CODE>",CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.Song2.Code)
                    .Replace("<START>", CurrentRound.RoundNumber == 1?CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.Song2.InitialStarttime.ToString(): CurrentRound.Battles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo)?.Song2.Starttime.ToString());
                File.WriteAllText(Path.Combine(path, "left.html"), left);
                File.WriteAllText(Path.Combine(path, "right.html"), right);
            }
        }
        
        public void PrepareWinner()
        {
            if (CurrentRound != null)
            {
                Song winner = CurrentRound.FinishedBattles.FirstOrDefault(b => b.Position == CurrentRound.CurrentBattleNo-1)
                    ?.Winner;
                if (winner != null)
                {
                    string path = Properties.Settings.Default.WebserverPath;
                    string tmpl = File.ReadAllText(Path.Combine(path, "player.tmpl"));
                    string winnerHtml = tmpl.Replace("<CODE>",winner.Code)
                        .Replace("<START>", winner.InitialStarttime.ToString());
                    File.WriteAllText(Path.Combine(path, "winner.html"), winnerHtml);
                    GameFinished = true;
                    FinishTimestamp = DateTime.Now;
                    OnWinnerFound(new EventArgs());
                }
            }
        }

        internal static Game Init(string theme, int noOfSongs, int songsPerPerson, int championshipNumber, int previewTime, int votingTime, string minLength, string maxLength, bool sendWhispers, bool allowDoubles, bool allowVoteChanges)
        {
            Game newGame = new Game();
            newGame.Rounds.Clear();
            newGame.NoOfRounds = GameHelper.CalculateNoOfRounds(noOfSongs);
            newGame.NoOfSongs = noOfSongs;
            newGame.ChampionshipNumber = championshipNumber;
            newGame.PreviewTime = previewTime;
            newGame.VotingTime = votingTime;
            newGame.NoOfSongsPerPerson = songsPerPerson;
            newGame.Theme = theme;
            for (int i = 0; i < newGame.NoOfRounds; i++)
                newGame.Rounds.Add(new Round(i + 1));
            newGame.SubmittedSongs = new BindingList<Song>();
            newGame.PreSubmittedSongs = new List<Song>();
            newGame.InitTimestamp = DateTime.Now;
            newGame.SendWhispers = sendWhispers;
            newGame.AllowDoubles = allowDoubles;
            newGame.AllowVoteCorrection = allowVoteChanges;
            string[] parts = minLength.Split(':');
            if (parts.Length == 2)
            {
                int min = int.Parse(parts[0]);
                int sec = int.Parse(parts[1]);
                newGame.MinSongLength = new TimeSpan(0, min, sec);
            }
            parts = maxLength.Split(':');
            if (parts.Length == 2)
            {
                int min = int.Parse(parts[0]);
                int sec = int.Parse(parts[1]);
                newGame.MaxSongLength = new TimeSpan(0, min, sec);
            }
            return newGame;
        }
    }
}
