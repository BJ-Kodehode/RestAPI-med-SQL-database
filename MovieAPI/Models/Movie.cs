namespace MovieAPI.Models;

public class Movie
{
    public int Id { get; set; } // Primary key
    public string Title { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public string Poster { get; set; } = string.Empty;
}
