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
        
        public List<Battle> FinishedBattles { get; set; } = new List<Battle>();

    }
}
