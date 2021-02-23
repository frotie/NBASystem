using NBAManagement.Events;
using NBAManagement.Messages;
using NBAManagement.Services;
using NBAManagement.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels.Pages
{
    class VisitorMenuVM : ViewModelBase
    {
        private EventBus _eventBus;
        public VisitorMenuVM(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public ICommand GoTeams => new RelayCommand(async o =>
        {
            await _eventBus.Publish(new ChangePage(new TeamsMain()));
        });
    }
}
