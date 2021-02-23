using NBAManagement.Models.BaseModels;
using NBAManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NBAManagement.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для TeamsList.xaml
    /// </summary>
    public partial class TeamsList : UserControl
    {
        public static readonly DependencyProperty TeamsProperty =
            DependencyProperty.Register("Teams", typeof(ObservableCollection<Team>), typeof(TeamsList));

        public ObservableCollection<Team> Teams
        {
            get { return (ObservableCollection<Team>)GetValue(TeamsProperty); }
            set { SetValue(TeamsProperty, value); }
        }
        public TeamsList()
        {
            InitializeComponent();
            ViewModel = ((ViewModelLocator)TryFindResource("ViewModelLocator")).TeamListVM;
        }
        public TeamListVM ViewModel
        {
            get => (TeamListVM)Root.DataContext;
            set => Root.DataContext = value;
        }
    }
}
