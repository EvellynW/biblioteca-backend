using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryContext _context;

    public BookController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        var books = await _context.Books.Include(x => x.LiteraryGenre).ToListAsync();
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Publisher = b.Publisher,
            Title = b.Title,
            ISBN = b.ISBN,
            Authors = b.Authors,
            PublicationYear = b.PublicationYear,
            Summary = b.Summary,
            Quantity = b.Quantity,
            literaryGenre = new LiteraryGenreDto { Id = b.LiteraryGenre.Id, Name = b.LiteraryGenre.Name }
        }).ToList();
    }

    // GET: api/book/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {

        var book = await _context.Books.Include(x => x.LiteraryGenre).FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return new BookDto
        {
            Id = book.Id,
            Publisher = book.Publisher,
            Title = book.Title,
            ISBN = book.ISBN,
            Authors = book.Authors,
            PublicationYear = book.PublicationYear,
            Summary = book.Summary,
            Quantity = book.Quantity,
            literaryGenre = new LiteraryGenreDto { Id = book.LiteraryGenre.Id, Name = book.LiteraryGenre.Name }

        };
    }

    // POST: api/book
    [HttpPost]
    public async Task<ActionResult<BookDto>> PostBook(BookDto bookDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var book = new Book
        {
            Publisher = bookDto.Publisher,
            Title = bookDto.Title,
            ISBN = bookDto.ISBN,
            Authors = bookDto.Authors,
            PublicationYear = bookDto.PublicationYear,
            Summary = bookDto.Summary,
            Quantity = bookDto.Quantity,
            LiteraryGenreId = (int)bookDto.literaryGenre.Id,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
    }

    // PUT: api/book/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, BookDto bookDto)
    {

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        book.Publisher = bookDto.Publisher;
        book.Title = bookDto.Title;
        book.ISBN = bookDto.ISBN;
        book.Authors = bookDto.Authors;
        book.PublicationYear = bookDto.PublicationYear;
        book.Summary = bookDto.Summary;
        book.Quantity = bookDto.Quantity;
        book.LiteraryGenre = await _context.LiteraryGenres.FirstOrDefaultAsync(b => b.Id == bookDto.Id);
        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/book/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
