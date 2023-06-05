using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Converters;
using Labyrinth.Data;

namespace Labyrinth.Models
{
    public class MapModel
    {
        private static readonly string mapsPath = "assets/Maps/maps.json";
        private static readonly ObservableCollection<MapModel> maps = GetMaps();

        public char[,] map;

        private MapModel(char[,] map)
        {
            this.map = map;
        }

        public static MapModel GetMap(int index)
        {
            return maps[index];
        }

        public static int GetMapsCount()
        {
            return maps.Count;
        }

        private static ObservableCollection<MapModel> GetMaps()
        {
            ObservableCollection<MapModel> maps = new();
            CharArrayToObservableCollectionConverter converter = new();

            foreach (var map in JsonManager.ReadListFromJsonFile<ObservableCollection<ObservableCollection<string>>>(mapsPath))
            {
                maps.Add(new MapModel((char[,]) converter.ConvertBack(map, null, null, null)));
            }

            return maps;
        }
    }
}
