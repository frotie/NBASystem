using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class Matchup
    {
        public int MatchupId { get; set; }
        public int SeasonId { get; set; }
        public int MatchupTypeId { get; set; }
        public int TeamAwayId { get; set; }
        public int TeamHomeId { get; set; }
        public DateTime StartTime { get; set; }
        public int TeamAwayScore { get; set; }
        public int TeamHomeScore { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public string CurrentQuarter { get; set; }
    }
}
