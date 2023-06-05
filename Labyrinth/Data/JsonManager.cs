using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Labyrinth.Converters;

namespace Labyrinth.Data
{
    public class JsonManager
    {

        public static ObservableCollection<T> ReadListFromJsonFile<T>(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (File.Exists(filePath))
            {
                return JsonDeserialize<T>(filePath, options);
            }
            else
            {
                File.Create(filePath).Close();
                return new ObservableCollection<T> { };
            }
        }

        private static ObservableCollection<T> JsonDeserialize<T>(string filePath, JsonSerializerOptions options)
        {
            string jsonString = File.ReadAllText(filePath);
            try
            {
                return JsonSerializer.Deserialize<ObservableCollection<T>>(jsonString, options);
            }
            catch (JsonException)
            {
                if (jsonString != "")
                {
                    ShowReadErrorMessage();
                }
                return new ObservableCollection<T> { };
            }
        }

        private static void ShowReadErrorMessage()
        {
            MessageBox.Show("Ошибка при чтении уровней из файла!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
