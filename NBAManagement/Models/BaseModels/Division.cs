using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models.BaseModels
{
    class Division
    {
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public int ConferenceId { get; set; }
    }
}
