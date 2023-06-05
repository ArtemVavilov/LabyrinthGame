using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Labyrinth.Converters
{
    public class CharArrayToObservableCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not char[,] array) 
                return null;
            ObservableCollection<ObservableCollection<string>> observableCollection = new();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                ObservableCollection<string> collection = new();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    collection.Add(array[i, j].ToString());
                }
                observableCollection.Add(collection);
            }
            return observableCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ObservableCollection<ObservableCollection<string>> observableCollection)
                return null;

            int rows = observableCollection.Count;
            int columns = observableCollection[0].Count;
            char[,] array = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _ = char.TryParse(observableCollection[i][j], out char charValue);
                    array[i, j] = charValue;
                }
            }
            return array;
        }
    }
}
