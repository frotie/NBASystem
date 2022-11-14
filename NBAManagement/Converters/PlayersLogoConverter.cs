using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NBAManagement.Converters
{
    class PlayersLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = "Resources/Players/" + value as string;

            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return "/NBAManagement;component/Resources/Images/person.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
