using NBAManagement.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    class BasketballSystemContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<PlayerInTeam> PlayersInTeams { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Matchup> Matchups { get; set; }
        public DbSet<MatchupType> MatchupTypes { get; set; }
        public BasketballSystemContext() : base("DefaultConnection")
        {
        }
    }
}
