namespace MovieFinder.Logic.Models
{
    public class ActorDto
    {
        public string FirstName {  get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName { get => $"{FirstName} {LastName}".Trim(); }
    }
}
