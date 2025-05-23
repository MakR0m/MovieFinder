using MovieFinder.Mobile.Services.Interfaces;
using System.Text.Json;

namespace MovieFinder.Mobile.Services
{
    public class PosterService : IPosterService // Сервис для определения пути к постеру фильма по названию
    {
        private readonly Dictionary<string, string> _posterMap = new();

        public PosterService()
        {
            LoadPostersFromJson(); 
        }

        private void LoadPostersFromJson()
        {
            try
            {
                // Получаем сборку, в которой содержится встроенный ресурс (в данном случае JSON-файл с постерами)
                // Используем typeof(PosterService).Assembly, чтобы путь не зависел от конкретного имени проекта или сборки
                // Основная проблема: файловая система андроид изолирована и получить доступ к пути вида /resources/ не получилось.
                var assembly = typeof(PosterService).Assembly;

                var resourceName = "MovieFinder.Mobile.Resources.Raw.posters.json"; //Важно, чтобы ресурс был типа EmbeddedResource

                using var stream = assembly.GetManifestResourceStream(resourceName); // Пытаемся открыть встроенный ресурс как поток
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
            return _posterMap.TryGetValue(title, out var path)  // Если название фильма есть в словаре, то вернуть путь, иначе unknown.jpg
                ? Path.Combine("Resources", "Posters", path)
                : Path.Combine("Resources", "Posters", "unknown.jpg");
        }
    }
}
