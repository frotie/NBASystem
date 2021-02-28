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
        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                UpdateData();
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
        private int _pagesCount;
        public int PagesCount {
            get => _pagesCount;
            set
            {
                _pagesCount = value;
                OnPropertyChanged();
            }
        }
        private int _totalRecords;
        public int TotalRecords {
            get => _totalRecords;
            set
            {
                _totalRecords = value;
                OnPropertyChanged();
            }
        }
        private uint _rowsInPage;
        public uint RowsInPage {
            get => _rowsInPage;
            set
            {
                _rowsInPage = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<int> AllPages { get; set; }
        private string _sortLetter;
        #endregion
        private BasketballSystemContext _db;
        public DbPagination<Player> Pagination { get; private set; }
        public PlayersPageVM()
        {
            _db = new BasketballSystemContext();
            _sortLetter = "";

            LetterButtons = new ObservableCollection<string>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => ((char)c).ToString()).ToList());
            LetterButtons.Insert(0, "ALL");
            Pagination = new DbPagination<Player>(_db);
            Players = new ObservableCollection<PlayerData>();
            AllPages = new ObservableCollection<int>();


            Seasons = new ObservableCollection<Season>(_db.Seasons);
            SelectedSeason = _db.Seasons.FirstOrDefault();
        }

        private void UpdateData()
        {
            Pagination.SetPredicate(
                p => _db.PlayersInTeams
                .Where(pit => pit.PlayerId == p.PlayerId)
                .Any()
                && p.Name.StartsWith(_sortLetter)
            );

            SetAllPages();
            UpdateTable();
        }

        private void SetAllPages()
        {
            AllPages.Clear();
            for (int i = 0; i < Pagination.PagesCount; i++)
            {
                AllPages.Add(i + 1);
            }

            PagesCount = Pagination.PagesCount;
            TotalRecords = Pagination.TotalRecords;
            RowsInPage = Pagination.RowsInPage;
            OnPropertyChanged("CurrentPage");
        }

        private void UpdateTable()
        {
            Players.Clear();

            foreach (var player in Pagination.GetPage(CurrentPage))
            {
                PlayerData pd = new PlayerData();

                pd.Player = player;
                pd.PlayerInTeam = _db.PlayersInTeams.Where(pit => pit.PlayerId == player.PlayerId).FirstOrDefault();
                pd.Coutry = _db.Countries.Where(c => c.CountryCode == player.CountryCode).Select(c => c.CountryName).FirstOrDefault();
                pd.Experience = DateTime.Now.Year - Convert.ToInt32(SelectedSeason.Name.Split('-')[0]);
                pd.Position = _db.Positions.Where(p => p.PositionId == pd.Player.PositionId).FirstOrDefault().Name;
                pd.Team = _db.Teams.Where(t => t.TeamId == pd.PlayerInTeam.TeamId).Select(t => t.TeamName).FirstOrDefault();

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
        }, o => CurrentPage < Pagination.PagesCount);
        
        public ICommand PaginationGoEnd => new RelayCommand(o =>
        {
            CurrentPage = Pagination.PagesCount;
        }, o => CurrentPage != Pagination.PagesCount);

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
