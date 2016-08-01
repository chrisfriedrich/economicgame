using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economic_Game.Models
{
    [Table("GameSettings")]
    public class GameSettings
    {
        public int GameSettingsID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal StartingAmount { get; set; }
        public int Round1Multiplier { get; set; }
        public int Round2Multiplier { get; set; }
        public int Round3Multiplier { get; set; }
        public int Round4Multiplier { get; set; }

        public decimal Round1ReturnPercentage { get; set; }
        public decimal Round2ReturnPercentage { get; set; }
        public decimal Round3ReturnPercentage { get; set; }
        public decimal Round4ReturnPercentage { get; set; }

        public string ComputerApology { get; set; }
        public string ComputerPersuasion { get; set; }

        public bool? SelectedSettings { get; set; }
    }
}
