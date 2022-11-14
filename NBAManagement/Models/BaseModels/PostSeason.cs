using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class PostSeason
    {
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int Rank { get; set; }
    }
}
