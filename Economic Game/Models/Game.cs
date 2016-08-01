using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economic_Game.Models
{
    [Table("Games")]
    public class Game
    {
        public int GameID { get; set; }
        public string PlayerName { get; set; }
        public string AmazonID { get; set; }
        public decimal? StartingAmount { get; set; }
        public int? CurrentRound { get; set; }

        public int? Round1Multiplier { get; set; }
        public int? Round2Multiplier { get; set; }
        public int? Round3Multiplier { get; set; }
        public int? Round4Multiplier { get; set; }

        public decimal? Round1ReturnPercentage { get; set; }
        public decimal? Round2ReturnPercentage { get; set; }
        public decimal? Round3ReturnPercentage { get; set; }
        public decimal? Round4ReturnPercentage { get; set; }

        public decimal? Round1Investment { get; set; }
        public decimal? Round1Kept { get; set; }
        public decimal? Round1Returned { get; set; }
        public decimal? Round2Investment { get; set; }
        public decimal? Round2Kept { get; set; }
        public decimal? Round2Returned { get; set; }
        public decimal? Round3Investment { get; set; }
        public decimal? Round3Kept { get; set; }
        public decimal? Round3Returned { get; set; }
        public decimal? Round4Investment { get; set; }
        public decimal? Round4Kept { get; set; }
        public decimal? Round4Returned { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal? PlayerTotal { get; set; }
        public decimal? ComputerTotal { get; set; }

        public string ComputerPersuasion { get; set; }
        public string ComputerApology { get; set; }

        public int? GameSettingsID { get; set; }
        public Game()
        {
            this.CurrentRound = 1;
        }
    }

}
