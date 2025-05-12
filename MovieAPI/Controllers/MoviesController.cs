using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using System.Net.Http;
using System.Text.Json;

namespace MovieAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MovieDbContext _context;
    private readonly HttpClient _httpClient;
    private const string OmdbApiKey = "441e592b"; // Replace with your OMDb API key

    public MoviesController(MovieDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    [HttpGet("search")] // Endpoint: api/movies/search?title=movieTitle
    public async Task<IActionResult> SearchMovie(string title)
    {
        var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?t={title}&apikey={OmdbApiKey}");
        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Error fetching movie from OMDb API.");
        }

        var movieData = await response.Content.ReadAsStringAsync();
        var movie = JsonSerializer.Deserialize<Movie>(movieData);

        if (movie == null)
        {
            return NotFound("Movie not found.");
        }

        // Save to database
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return Ok(movie);
    }

    [HttpGet] // Endpoint: api/movies
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Ok(movies);
    }
}
