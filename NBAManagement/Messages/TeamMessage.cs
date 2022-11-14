using NBAManagement.Models.BaseModels;
using NBAManagement.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Messages
{
    class TeamMessage : IMessage
    {
        public Team CurrentTeam { get; set; }
        public TeamDetail SelectedDetail { get; set; }
        public TeamMessage(Team team = null, TeamDetail detail = TeamDetail.Lineup)
        {
            CurrentTeam = team;
            SelectedDetail = detail;
        }
    }
}
