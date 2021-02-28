using NBAManagement.Models;
using NBAManagement.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels.Pages
{
    class PlayersPageVM : ViewModelBase
    {
        public ObservableCollection<string> LetterButtons { get; set; }
        public ObservableCollection<Season> Seasons { get; set; }
        public ObservableCollection<PlayerData> Players { get; set; }

        #region Pagination
        public int ROWS_PER_PAGE { get; set; } = 10;
        private int _allRecordsCount;
        public int AllRecordsCount {
            get => _allRecordsCount;
            set
            {
                _allRecordsCount = value;
                OnPropertyChanged();
            }
        }
        private int _currentPage;
        public int CurrentPage {
            get => _currentPage;
            set
            {
                _currentPage = value;
                UpdateData();
            }
        }
        private string _sortLetter;
        public ObservableCollection<int> AllPages { get; set; }
        private int _pagesCount;
        public int PagesCount {
            get => _pagesCount;
            set
            {
                _pagesCount = value;
                OnPropertyChanged();
            }
        }
        private Season _selectedSeason;
        public Season SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;
                OnPropertyChanged();

                UpdateData();
            }
        }
        #endregion
        private BasketballSystemContext _db;
        public PlayersPageVM()
        {
            LetterButtons = new ObservableCollection<string>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => ((char)c).ToString()).ToList());
            LetterButtons.Insert(0, "ALL");
            _db = new BasketballSystemContext();
            _sortLetter = "";


            _db.Seasons.Load();
            _db.Players.Load();
            _db.PlayersInTeams.Load();
            _db.Countries.Load();
            _db.Positions.Load();
            _db.Teams.Load();

            Players = new ObservableCollection<PlayerData>();
            AllPages = new ObservableCollection<int>();

            Seasons = new ObservableCollection<Season>(_db.Seasons.Local);
            SelectedSeason = _db.Seasons.Local.FirstOrDefault();

            CurrentPage = 1;
        }

        private void UpdateData()
        {
            List<Player> allPlayers = new List<Player>();

            foreach (PlayerInTeam pit in _db.PlayersInTeams.Local
                .Where(p => p.SeasonId == SelectedSeason.SeasonId))
            {
                var pl = _db.Players.Local.Where(p => pit.PlayerId == p.PlayerId).First();

                if (pl.Name.ToLower().StartsWith(_sortLetter.ToLower()))
                    allPlayers.Add(pl);
            }

            SetAllPages(allPlayers.Count);
            UpdateTable(allPlayers);
        }

        private void SetAllPages(int records)
        {
            AllRecordsCount = records;
            PagesCount = (int)Math.Ceiling((double)AllRecordsCount / ROWS_PER_PAGE);

            AllPages.Clear();
            for (int i = 0; i < PagesCount; i++)
            {
                AllPages.Add(i + 1);
            }
            OnPropertyChanged("CurrentPage");
        }

        private void UpdateTable(IEnumerable<Player> players)
        {
            Players.Clear();
            foreach (var player in players.Skip((CurrentPage - 1) * ROWS_PER_PAGE).Take(ROWS_PER_PAGE))
            {
                PlayerData pd = new PlayerData();

                pd.Player = player;
                pd.PlayerInTeam = _db.PlayersInTeams.Local.Where(pit => pit.PlayerId == player.PlayerId).FirstOrDefault();
                pd.Coutry = _db.Countries.Where(c => c.CountryCode == player.CountryCode).Select(c => c.CountryName).FirstOrDefault();
                pd.Experience = DateTime.Now.Year - Convert.ToInt32(SelectedSeason.Name.Split('-')[0]);
                pd.Position = _db.Positions.Local.Where(p => p.PositionId == pd.Player.PositionId).FirstOrDefault().Name;
                pd.Team = _db.Teams.Local.Where(t => t.TeamId == pd.PlayerInTeam.TeamId).Select(t => t.TeamName).FirstOrDefault();

                Players.Add(pd);
            }
        }




        #region Commands
        public ICommand PaginationGoStart => new RelayCommand(o =>
        {
            CurrentPage = 1;
        }, o => CurrentPage != 1);

        public ICommand PaginationGoBack => new RelayCommand(o =>
        {
            CurrentPage = CurrentPage - 1;
        }, o => CurrentPage > 1);

        public ICommand PaginationGoForward => new RelayCommand(o =>
        {
            CurrentPage = CurrentPage + 1;
        }, o => CurrentPage < PagesCount);
        
        public ICommand PaginationGoEnd => new RelayCommand(o =>
        {
            CurrentPage = PagesCount;
        }, o => CurrentPage != PagesCount);

        public ICommand ChooseLetter => new RelayCommand(letter =>
        {
            if ((string)letter == "ALL")
            {
                _sortLetter = "";
            }
            else
            {
                _sortLetter = letter as string;
            }

            CurrentPage = 1;
        });
        #endregion
    }
}
