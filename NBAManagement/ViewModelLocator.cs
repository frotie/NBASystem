using NBAManagement.Services;
using NBAManagement.ViewModels;
using NBAManagement.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement
{
    class ViewModelLocator
    {
        private static EventBus _eventBus;
        private static MessageBus _messageBus;
        private static TeamDetailsVM _teamDetailsVM;

        public static void Init()
        {
            // Singleton
            _eventBus = new EventBus();
            _messageBus = new MessageBus();

            _teamDetailsVM = new TeamDetailsVM(_messageBus);
        }

        public MainViewModel MainViewModel => new MainViewModel(_eventBus);
        public MainPageVM MainPageVM => new MainPageVM(_eventBus);
        public VisitorMenuVM VisitorMenuVM => new VisitorMenuVM(_eventBus);
        public TeamsMainVM TeamsMainVM => new TeamsMainVM();
        public TeamDetailsVM TeamDetailsVM => _teamDetailsVM;
        public TeamListVM TeamListVM => new TeamListVM(_messageBus, _eventBus);

        public PlayersPageVM PlayersPageVM => new PlayersPageVM();
    }
}
