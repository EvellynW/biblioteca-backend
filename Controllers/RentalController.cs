using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly LibraryContext _context;

    public RentalController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/Rental
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
    {
        var rentals = await _context.Rentals
            .Select(r => new RentalDto
            {
                id = r.Id,
                StudentId = r.StudentId,
                Book = new BookDto
                {
                    Id = r.Book.Id,
                    Authors = r.Book.Authors,
                    ISBN = r.Book.ISBN,
                    Title = r.Book.Title,
                    PublicationYear = r.Book.PublicationYear,
                     Publisher = r.Book.Publisher,
                     Summary = r.Book.Summary,
                     literaryGenre = new LiteraryGenreDto{
                        Id = r.Book.LiteraryGenre.Id,
                        Name = r.Book.LiteraryGenre.Name,
                     }
                },
                RentalDate = r.RentalDate,
                DueDate = r.DueDate,
                ReturnDate = r.ReturnDate
            })
            .ToListAsync();

        return Ok(rentals);
    }

    // GET: api/Rental/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RentalDto>> GetRental(int id)
    {
        var rental = await _context.Rentals
            .Where(r => r.Id == id)
            .Select(r => new RentalDto
            {
                id = r.Id,
                StudentId = r.StudentId,
                 Book = new BookDto
                {
                    Id = r.Book.Id,
                    Authors = r.Book.Authors,
                    ISBN = r.Book.ISBN,
                    Title = r.Book.Title,
                    PublicationYear = r.Book.PublicationYear,
                     Publisher = r.Book.Publisher,
                     Summary = r.Book.Summary,
                     literaryGenre = new LiteraryGenreDto{
                        Id = r.Book.LiteraryGenre.Id,
                        Name = r.Book.LiteraryGenre.Name,
                     }
                },
                RentalDate = r.RentalDate,
                DueDate = r.DueDate,
                ReturnDate = r.ReturnDate
            })
            .FirstOrDefaultAsync();

        if (rental == null)
        {
            return NotFound();
        }

        return Ok(rental);
    }

    // POST: api/Rental
    [HttpPost]
    public async Task<ActionResult<RentalDto>> PostRental(RentalDto rentalDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var rental = new Rental
        {
            StudentId = rentalDto.StudentId,
            RentalDate = rentalDto.RentalDate,
            DueDate = rentalDto.DueDate,
            ReturnDate = rentalDto.ReturnDate,
            Book = await _context.Books.FindAsync(rentalDto.Book.Id)
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        rentalDto.id = rental.Id;

        return CreatedAtAction(nameof(GetRental), new { id = rentalDto.id }, rentalDto);
    }

    // PUT: api/Rental/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRental(int id, RentalDto rentalDto)
    {
        if (id != rentalDto.id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        rental.StudentId = rentalDto.StudentId;
        rental.RentalDate = rentalDto.RentalDate;
        rental.DueDate = rentalDto.DueDate;
        rental.ReturnDate = rentalDto.ReturnDate;
        rental.Book = await _context.Books.FindAsync(rentalDto.Book.Id);

        _context.Entry(rental).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Rental/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRental(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
