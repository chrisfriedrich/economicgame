using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Economic_Game.Models;

namespace Economic_Game.DAL
{
    public class EconomicGameDataContext : DbContext
    {
        public EconomicGameDataContext() : base("EconomicGameDataContext")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
