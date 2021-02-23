using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int PositionId { get; set; }
        public DateTime JoinYear { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string College { get; set; }
        public string CountryCode { get; set; }
        public string Img { get; set; }
        public bool IsRetirment { get; set; }
        public Nullable<DateTime> RetirmentTime { get; set; }
    }
}
