using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieFinder.Mobile.Services
{
    public class PosterService  // Сервис для определения постера к фильму
    {
        private readonly Dictionary<string, string> _posterMap = new();

        public PosterService()
        {
            LoadPostersFromJson();  // Считываем json в конструкторе
        }

        private void LoadPostersFromJson()
        {
            try
            {
                var assembly = typeof(PosterService).Assembly; 
                var resourceName = "MovieFinder.Mobile.Resources.Raw.posters.json";

                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                    return;

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                var posters = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (posters != null)
                {
                    foreach (var poster in posters)
                        _posterMap[poster.Key] = poster.Value;
                }
            }
            catch
            {
                return;  // Если не удалось просто выходим. Не думаю, что выкидывать ошибку без логгера из-за костыля с json целесообразно
            }
        }

        public string GetPosterPath(string title)
        {
            return _posterMap.TryGetValue(title, out var path)  // Если название фильма есть в словаре, то вернуть путь, иначе default.jpg
                ? Path.Combine("Resources", "Posters", path)
                : Path.Combine("Resources", "Posters", "unknown.jpg");
        }
    }
}
