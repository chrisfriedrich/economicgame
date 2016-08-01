using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economic_Game.Models
{
    public class LoginViewModel
    {
        public Game Game { get; set; }
        public string PlayerName { get; set; }
        [Required(ErrorMessage="You must enter your Amazon ID to continue.")]
        public string AmazonID { get; set; }
    }
}
