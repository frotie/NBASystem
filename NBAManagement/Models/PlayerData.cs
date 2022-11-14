using NBAManagement.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    class PlayerData
    {
        public Player Player { get; set; }
        public PlayerInTeam PlayerInTeam { get; set; }
        public int Experience { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Coutry { get; set; }
    }
}
