using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NBAManagement.Models.BaseModels;
using NBAManagement.Models;
using System.Data.Entity;

namespace NBAManagement.ViewModels.Pages
{
    class TeamsMainVM : ViewModelBase
    {
        public ObservableCollection<TeamsByConference> Conferences { get; set; }

        private BasketballSystemContext _db;
        public TeamsMainVM()
        {
            _db = new BasketballSystemContext();
            _db.Teams.Load();
            _db.Conferences.Load();
            _db.Divisions.Load();

            Conferences = new ObservableCollection<TeamsByConference>();

            foreach (Conference conf in _db.Conferences.Local)
            {
                var res = new TeamsByConference();

                res.Conference = conf;
                res.Divisions = new ObservableCollection<TeamsInDivision>();

                foreach(Division division in _db.Divisions.Local.Where(d => d.ConferenceId == conf.ConferenceId).ToList())
                {
                    res.Divisions.Add(new TeamsInDivision() { 
                        Division = division, 
                        Teams = new ObservableCollection<Team>(_db.Teams.Local.Where(t => t.DivisionId == division.DivisionId).ToList())
                    });
                }

                Conferences.Add(res);
            }
        }
    }

    class TeamsByConference
    {
        public Conference Conference { get; set; }
        public ObservableCollection<TeamsInDivision> Divisions { get; set; }
    }

    class TeamsInDivision
    {
        public Division Division { get; set; }
        public ObservableCollection<Team> Teams { get; set; }
    }
}
