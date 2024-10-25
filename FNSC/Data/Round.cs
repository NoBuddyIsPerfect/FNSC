using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FNSC.Data;


namespace FNSC.Data
{
    public class Round
    {
        
        public Round(int roundNo) { RoundNumber = roundNo; }

        public event EventHandler<Song> WinnerFound;
        public Round()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int RoundNumber { get; set; }
        public int CurrentBattleNo { get; set; } = 1;
        public Battle CurrentBattle { get; set; } 
        
        public List<Battle> Battles;
        [NotMapped] 
        public int BattleCount => Battles.Count + FinishedBattles.Count;
        public List<Battle> FinishedBattles { get; set; } = new List<Battle>();

        public void SetWinnerFromCoinflip(string result)
        {
            if(CurrentBattle == null)
                return;
            if (result.ToLower() == "left")
            {
                CurrentBattle.SetWinner(1);
                
            }else if (result.ToLower() == "right")
            {
                CurrentBattle.SetWinner(2);
            }
            OnWinnerFound(CurrentBattle.Winner);
        }

        private void OnWinnerFound(Song args)
        {

            EventHandler<Song> handler = WinnerFound;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public string Export()
        {
            string export = "{";
            export += "\"Matches\":{";
            foreach (Battle match in Battles)
                export += match.Export() + ",";
            export = export.TrimEnd(',');
            export += "},";
            export += "\"FinishedMatches\":{";
            foreach (Battle match in FinishedBattles)
                export += match.Export() + ",";
            export = export.TrimEnd(',');
            export += "}";
            export += "}";
            return export;

        }
    }
}
