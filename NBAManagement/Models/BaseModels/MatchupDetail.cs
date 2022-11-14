using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class MatchupDetail
    {
        public int Id { get; set; }
        public int MatchupId { get; set; }
        public int Quarter { get; set; }
        public int TeamAwayScore { get; set; }
        public int TeamHomeScore { get; set; }
    }
}
