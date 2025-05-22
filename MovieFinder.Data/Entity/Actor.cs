namespace MovieFinder.Data.Entity
{
    public class Actor
    {
        public int Id { get; set; }                          
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;  

        public List<Movie> Movies { get; set; } = new();      //Список ссылок на фильмы (для создания внешних ключей)
    }
}
