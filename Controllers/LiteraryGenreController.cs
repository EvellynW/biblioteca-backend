using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LiteraryGenreController : ControllerBase
{
    private readonly LibraryContext _context;

    public LiteraryGenreController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/literarygenre
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LiteraryGenreDto>>> GetLiteraryGenres()
    {
        var genres = await _context.LiteraryGenres.ToListAsync();

        return genres.Select(g => new LiteraryGenreDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();
    }

    // GET: api/literarygenre/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<LiteraryGenreDto>> GetLiteraryGenre(int id)
    {
        var genre = await _context.LiteraryGenres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return new LiteraryGenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    // POST: api/literarygenre
    [HttpPost]
    public async Task<ActionResult<LiteraryGenreDto>> PostLiteraryGenre(LiteraryGenreDto genreDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var genre = new LiteraryGenre(genreDto.Name);

        _context.LiteraryGenres.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLiteraryGenre), new { id = genre.Id }, genreDto);
    }

    // PUT: api/literarygenre/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLiteraryGenre(int id, LiteraryGenreDto genreDto)
    {
        var genre = await _context.LiteraryGenres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        genre.Name = genreDto.Name;

        _context.Entry(genre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.LiteraryGenres.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                return BadRequest("Ocorreu um erro ao atualizar o gênero literário");
            }
        }

        return NoContent();
    }

    // DELETE: api/literarygenre/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLiteraryGenre(int id)
    {
        var genre = await _context.LiteraryGenres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        _context.LiteraryGenres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LiteraryGenreExists(int id)
    {
        return _context.LiteraryGenres.Any(e => e.Id == id);
    }
}
