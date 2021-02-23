using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class PlayerStatistics
    {
        public int Id { get; set; }
        public int MatchupId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public bool IsStarting { get; set; }
        public int Min { get; set; }
        public int Point { get; set; }
        public int Assist { get; set; }
        public int FieldGoalMade { get; set; }
        public int FieldGoalMissed { get; set; }
        public int ThreePointFieldGoalMade { get; set; }
        public int ThreePointFieldGoalMissed { get; set; }
        public int FreeThrowMade { get; set; }
        public int FreeThrowMissed { get; set; }
        public int Rebound { get; set; }
        public int OFFR { get; set; }
        public int DFFR { get; set; }
        public int Steal { get; set; }
        public int Block { get; set; }
        public int Turnover { get; set; }
        public int Foul { get; set; }
    }
}