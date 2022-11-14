using NBAManagement.Events;
using NBAManagement.Messages;
using NBAManagement.Models.BaseModels;
using NBAManagement.Services;
using NBAManagement.ViewModels.Pages;
using NBAManagement.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels
{
    public class TeamListVM : ViewModelBase
    {
        private MessageBus _messageBus;
        private EventBus _eventBus;
        public TeamListVM(MessageBus messageBus, EventBus eventBus)
        {
            _messageBus = messageBus;
            _eventBus = eventBus;
        }

        public ICommand RosterClick => new RelayCommand(async o =>
        {
            await _messageBus.SendTo<TeamDetailsVM>(new TeamMessage((Team)o, Models.Enums.TeamDetail.Roster));
            await _eventBus.Publish(new ChangePage(new TeamDetails()));
        });
        public ICommand MatchupClick => new RelayCommand(async o =>
        {
            await _messageBus.SendTo<TeamDetailsVM>(new TeamMessage((Team)o, Models.Enums.TeamDetail.Matchup));
            await _eventBus.Publish(new ChangePage(new TeamDetails()));
        });
        public ICommand LineupClick => new RelayCommand(async o =>
        {
            await _messageBus.SendTo<TeamDetailsVM>(new TeamMessage((Team)o, Models.Enums.TeamDetail.Lineup));
            await _eventBus.Publish(new ChangePage(new TeamDetails()));
        });
    }
}
