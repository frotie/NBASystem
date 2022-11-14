using NBAManagement.Events;
using NBAManagement.Messages;
using NBAManagement.Services;
using NBAManagement.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NBAManagement.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private const int YearOfFoundation = 1946;
        private Page _previousPage;
        private Page _currentPage;
        public Page CurrentPage {
            get => _currentPage;
            set
            {
                _previousPage = _currentPage;
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        public string CurrentSeasonis { get; set; }
        public int YearsCount { get; set; }
        public System.Windows.Navigation.NavigationService NavigationService { get; set; }

        private EventBus _eventBus;
        public MainViewModel(EventBus eventBus)
        {
            _eventBus = eventBus;
            SeasonisCount();
            CurrentPage = new MainPage();

            _eventBus.Subscribe<ChangePage>(async @event =>
            {
                CurrentPage = @event.NextPage;
            });
        }

        private void SeasonisCount()
        {
            if(DateTime.Now.Month >= 9)
            {
                CurrentSeasonis = DateTime.Now.Year + " - " + (DateTime.Now.Year + 1);
            }
            else
            {
                CurrentSeasonis = (DateTime.Now.Year - 1) + " - " + DateTime.Now.Year;
            }
            YearsCount = DateTime.Now.Year - YearOfFoundation;
        }

        public ICommand GoBack => new RelayCommand(o =>
        {
            if(_previousPage != null)
                CurrentPage = _previousPage;
        }, o => _previousPage != null);
    }
}
