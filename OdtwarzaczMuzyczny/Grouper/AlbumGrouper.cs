using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdtwarzaczMuzyczny
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Globalization;

    class AlbumGrouper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String letter = value.ToString();
            if (letter.Length == 0)
                return "*";
            if ((letter[0] >= 'A' && letter[0] <= 'Z') || (letter[0] >= 'a' && letter[0] <= 'z') || (letter[0] >= '0' && letter[0] <= '9'))
            {
                return letter[0];
            }
            return "*";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
