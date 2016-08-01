using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economic_Game.Models
{
    public class RoundViewModel
    {
        public int GameID { get; set; }

        public string AmazonID { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal? StartingAmount { get; set; }
        public int? CurrentRound { get; set; }
        public string RoundType { get; set; }

        public int? Multiplier { get; set; }
        public string MultiplierText { get; set; }

        public decimal? RoundInvestment { get; set; }
        public decimal? RoundKept { get; set; }
        public decimal? RoundReturned { get; set; }
        public decimal? ReturnPercentage { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal? PlayerTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal? CurrentEarnings { get; set; }

        public string Message1 { get; set; }
        public string Message2 { get; set; }
        public string Message3 { get; set; }

        public string ComputerPersuasion { get; set; }
        public string ComputerApology { get; set; }

    }
}
