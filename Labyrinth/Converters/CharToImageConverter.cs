using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Labyrinth.Converters
{
    public class CharToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string) value switch {
                "#" => "../../Assets/Images/wall.png", 
                "*" => "../../Assets/Images/player.png",
                "1" => "../../Assets/Images/star.png",
                _ => "../../Assets/Images/empty.png"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
