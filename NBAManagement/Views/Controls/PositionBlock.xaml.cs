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
    /// Логика взаимодействия для PositionBlock.xaml
    /// </summary>
    public partial class PositionBlock : UserControl
    {
        public static DependencyProperty PositionAbbrProperty = DependencyProperty.Register("PositionAbbr", typeof(string), typeof(PositionBlock));
        public static DependencyProperty PlayersProperty = DependencyProperty.Register("Players", typeof(ObservableCollection<string>), typeof(PositionBlock));
        public string PositionAbbr
        {
            get => (string)GetValue(PositionAbbrProperty);
            set => SetValue(PositionAbbrProperty, value);
        }
        public ObservableCollection<string> Players
        {
            get => (ObservableCollection<string>)GetValue(PlayersProperty);
            set => SetValue(PlayersProperty, value);
        }
        public PositionBlock()
        {
            InitializeComponent();
        }
    }
}
