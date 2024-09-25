using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FNSC.Data
{
    public class Battle
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public Song Song1 { get; set; }

        public int Votes1 { get; set; }

        public Song Song2 { get; set; }

        public int Votes2 { get; set; }

     
        public Song Winner { get; set; }
        
        public Dictionary<Viewer, string> Voted = new Dictionary<Viewer, string>();
        public int Position { get; set; }

        public int RoundId { get; set; }
        public Round Round { get; set; }

        public void CloseVoting()
        {
            if (Votes1 != Votes2)
            {
                SetWinner(Votes1 > Votes2 ? 1 : 2);
            }
        }

        public void ResetVoting()
        {
            Votes1 = 0;
            Votes2 = 0;
            Voted.Clear();
        }

        public void SetWinner(int winner)
        {
            if (winner == 1)
            {
                Winner = Song1;
                Song2.isOut = Song1.won = true;
            }else if(winner == 2)
            {
                Winner = Song2;
                Song1.isOut = Song2.won = true;
            }
            if(Winner != null)
                Winner.Starttime = Winner.InitialStarttime + 90 +
                                   ((Round.RoundNumber - 1) * 30);
        }
    }
}
