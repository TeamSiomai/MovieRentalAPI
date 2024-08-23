using Microsoft.AspNetCore.Mvc;
using MovieRentalsAPI.DBContext;
using MovieRentalsAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentalsAPI.Controllers
{
    public class MovieController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class MoviesController : ControllerBase
        {
            private readonly MovieRentalContext _context;

            public MoviesController(MovieRentalContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
            {
                return await _context.Movies.Where(m => !m.IsDeleted).ToListAsync();
            }

            [HttpGet("rented")]
            public async Task<ActionResult<IEnumerable<Movie>>> GetRentedMovies()
            {
                return await _context.Movies.Where(m => m.IsRented && !m.IsDeleted).ToListAsync();
            }

            [HttpGet("notrented")]
            public async Task<ActionResult<IEnumerable<Movie>>> GetNotRentedMovies()
            {
                return await _context.Movies.Where(m => !m.IsRented && !m.IsDeleted).ToListAsync();
            }

            [HttpPost]
            public async Task<ActionResult<Movie>> AddMovie(Movie movie)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMovies), new { id = movie.MovieId }, movie);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateMovie(int id, Movie movie)
            {
                if (id != movie.MovieId)
                    return BadRequest();

                _context.Entry(movie).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Movies.Any(e => e.MovieId == id))
                        return NotFound();
                    else
                        throw;
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteMovie(int id)
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                    return NotFound();

                movie.IsDeleted = true;
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}
