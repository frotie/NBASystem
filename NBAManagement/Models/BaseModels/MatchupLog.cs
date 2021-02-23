using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class MatchupLog
    {
        public int Id { get; set; }
        public int MatchupId { get; set; }
        public int Quarter { get; set; }
        public string OccurTime { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public int ActionTypeId { get; set; }
        public string Remark { get; set; }
    }
}
