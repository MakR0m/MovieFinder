namespace MovieFinder.Data.Entity
{
    public class Movie
    {
        public int Id { get; set; }                           
        public string Title { get; set; } = string.Empty;       
        public string Genre { get; set; } = string.Empty;      
        public string Description { get; set; } = string.Empty; 
        public TimeSpan Duration { get; set; }                  //Продолжительность в секундах

        public List<Actor> Actors { get; set; } = new(); //Список ссылок на актеров (для создания внешних ключей)
    }
}
