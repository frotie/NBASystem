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

        // Pagination
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
                OnPropertyChanged();
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

            UpdateData();
        }

        public ICommand ChooseLetter => new RelayCommand(letter =>
        {
            if ((string)letter == "ALL")
            {
                _sortLetter = "";
            }
            else {
                _sortLetter = letter as string;
            }

            UpdateData();
        });

        private void UpdateData(int page = 1)
        {
            List<Player> allPlayers = new List<Player>();

            foreach (PlayerInTeam pit in _db.PlayersInTeams.Local.Where(p => p.SeasonId == SelectedSeason.SeasonId))
            {
                if(_db.Players.Local.Where(p => pit.PlayerId == p.PlayerId && p.Name.ToLower().StartsWith(_sortLetter.ToLower())).Any())
                    allPlayers.Add(_db.Players.Local.Where(p => pit.PlayerId == p.PlayerId && p.Name.ToLower().StartsWith(_sortLetter.ToLower())).FirstOrDefault());
            }

            AllRecordsCount = allPlayers.Count();
            PagesCount = (int)Math.Ceiling((double)AllRecordsCount / ROWS_PER_PAGE);

            AllPages.Clear();
            for (int i = 1; i < PagesCount + 1; i++)
            {
                AllPages.Add(i);
            }

            Players.Clear();
            foreach (var player in allPlayers.Skip((page - 1) * ROWS_PER_PAGE).Take(ROWS_PER_PAGE))
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

            CurrentPage = page;
        }

        public ICommand PaginationGoStart => new RelayCommand(o =>
        {
            UpdateData(1);
        }, o => CurrentPage != 1);

        public ICommand PaginationGoBack => new RelayCommand(o =>
        {
            UpdateData(CurrentPage - 1);
        }, o => CurrentPage > 1);

        public ICommand PaginationGoForward => new RelayCommand(o =>
        {
            UpdateData(CurrentPage + 1);
        }, o => CurrentPage < PagesCount);

        public ICommand PaginationGoEnd => new RelayCommand(o =>
        {
            UpdateData(PagesCount);
        }, o => CurrentPage != PagesCount);
    }
}
