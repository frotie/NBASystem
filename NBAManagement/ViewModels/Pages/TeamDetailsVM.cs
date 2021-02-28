using NBAManagement.Messages;
using NBAManagement.Models;
using NBAManagement.Models.BaseModels;
using NBAManagement.Models.Enums;
using NBAManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.ViewModels.Pages
{
    class TeamDetailsVM : ViewModelBase
    {
        public Team CurrentTeam { get; set; }
        public Division CurrentDivision { get; set; }
        public Conference CurrentConference { get; set; }

        public ObservableCollection<PlayerData> Players { get; set; }
        public ObservableCollection<MatchupData> Matchups { get; set; }

        public int SelectedDetail { get; set; }

        public ObservableCollection<Season> Seasons { get; set; }
        private Season _selectedSeason;
        public Season SelectedSeason {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;
                OnPropertyChanged();

                UpdateData();
            }
        }
        private Dictionary<Position, ObservableCollection<string>> _playersByPosition;
        public Dictionary<Position, ObservableCollection<string>> PlayersByPosition
        {
            get => _playersByPosition;
            set
            {
                _playersByPosition = value;
                OnPropertyChanged();
            }
        }

        private MessageBus _messageBus;
        private BasketballSystemContext _db;
        public TeamDetailsVM(MessageBus messageBus)
        {
            _messageBus = messageBus;
            Players = new ObservableCollection<PlayerData>();
            Matchups = new ObservableCollection<MatchupData>();
            PlayersByPosition = new Dictionary<Position, ObservableCollection<string>>(5);

            _messageBus.Receive<TeamMessage>(this, async team =>
            {
                _db = new BasketballSystemContext();
                CurrentTeam = team.CurrentTeam;
                SelectedDetail = (int)team.SelectedDetail;

                // Loading and initializing data
                CurrentDivision = _db.Divisions.Where(d => d.DivisionId == CurrentTeam.DivisionId).FirstOrDefault();
                CurrentConference = _db.Conferences.Where(c => c.ConferenceId == CurrentDivision.ConferenceId).FirstOrDefault();

                Seasons = new ObservableCollection<Season>(_db.Seasons);
                SelectedSeason = _db.Seasons.FirstOrDefault();

                UpdateData();
            });
        }

        private void UpdatePlayers()
        {
            Players.Clear();
            foreach (PlayerInTeam pit in _db.PlayersInTeams.Where(p => p.SeasonId == SelectedSeason.SeasonId && p.TeamId == CurrentTeam.TeamId))
            {
                PlayerData pd = new PlayerData();

                pd.Player = _db.Players.Where(p => p.PlayerId == pit.PlayerId).FirstOrDefault();
                pd.PlayerInTeam = pit;
                pd.Position = _db.Positions.Where(p => p.PositionId == pd.Player.PositionId).FirstOrDefault().Name;
                pd.Experience = DateTime.Now.Year - Convert.ToInt32(SelectedSeason.Name.Split('-')[0]);

                Players.Add(pd);
            }
        }

        private void UpdateMatchups()
        {
            Matchups.Clear();
            foreach(var matchup in _db.Matchups.Where(
                m => m.SeasonId == SelectedSeason.SeasonId && 
                (m.TeamHomeId == CurrentTeam.TeamId || m.TeamAwayId == CurrentTeam.TeamId)))
            {
                MatchupData md = new MatchupData();

                md.Matchup = matchup;
                md.MatchupType = _db.MatchupTypes.Where(mt => mt.MatchupTypeId == matchup.MatchupTypeId).FirstOrDefault();
                md.Opponent = _db.Teams.Where(t => (matchup.TeamHomeId == t.TeamId || matchup.TeamAwayId == t.TeamId) && t.TeamId != CurrentTeam.TeamId).FirstOrDefault();

                Matchups.Add(md);
            }
        }

        private void UpdateLineup()
        {
            PlayersByPosition.Clear();
            foreach (var position in _db.Positions)
            {
                PlayersByPosition.Add(position, new ObservableCollection<string>(Players
                    .Where(p => p.Player.PositionId == position.PositionId)
                    .Select(p => p.Player.Name)
                    .ToList()));
            }
        }

        private void UpdateData()
        {
            UpdatePlayers();
            UpdateMatchups();
            UpdateLineup();
        }
    }

    class MatchupData
    {
        public Matchup Matchup { get; set; }
        public Team Opponent { get; set; }
        public MatchupType MatchupType { get; set; }
    }
}
