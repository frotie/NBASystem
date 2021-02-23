using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public string Coach { get; set; }
        public string Abbr { get; set; }
        public string Stadium { get; set; }
        public string Logo { get; set; }
    }
}
