using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class PlayerInTeam
    {
        public int PlayerInTeamId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int ShirtNumber { get; set; }
        public int Salary { get; set; }
        public int StarterIndex { get; set; }
    }
}
