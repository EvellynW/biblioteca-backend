using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class BookRecommendationController : ControllerBase
{
    private readonly LibraryContext _context;

    public BookRecommendationController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/bookrecommendation
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookRecommendationDto>>> GetBookRecommendations()
    {
        var recommendations = await _context.BookRecommendations
            .Include(br => br.Teacher)
            .Include(br => br.Book)
            .Include(br => br.Classroom)
            .ToListAsync();

        return recommendations.Select(br => new BookRecommendationDto
        {
            id = br.Id,
            TeacherId = br.Teacher.Id, // Obter o ID do professor
            Book = new BookDto
            {
                Id = br.Book.Id,
                Publisher = br.Book.Publisher,
                Title = br.Book.Title,
                ISBN = br.Book.ISBN,
                Authors = br.Book.Authors,
                PublicationYear = br.Book.PublicationYear,
                Summary = br.Book.Summary,
                Quantity = br.Book.Quantity
            },
            RecommendationDate = br.RecommendationDate,
            Description = br.Description,
            Classroom = new ClassRoomDto
            {
                Id = br.Classroom.Id,
                Description = br.Classroom.Description,
                Shift = br.Classroom.Shift
            }
        }).ToList();
    }

    // GET: api/bookrecommendation/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<BookRecommendationDto>> GetBookRecommendation(int id)
    {
        var recommendation = await _context.BookRecommendations
            .Include(br => br.Teacher)
            .Include(br => br.Book)
            .Include(br => br.Classroom)
            .FirstOrDefaultAsync(br => br.Id == id);

        if (recommendation == null)
        {
            return NotFound();
        }

        return new BookRecommendationDto
        {
            id = recommendation.Id,
            TeacherId = recommendation.Teacher.Id, // Obter o ID do professor
            Book = new BookDto
            {
                Id = recommendation.Book.Id,
                Publisher = recommendation.Book.Publisher,
                Title = recommendation.Book.Title,
                ISBN = recommendation.Book.ISBN,
                Authors = recommendation.Book.Authors,
                PublicationYear = recommendation.Book.PublicationYear,
                Summary = recommendation.Book.Summary,
                Quantity = recommendation.Book.Quantity
            },
            RecommendationDate = recommendation.RecommendationDate,
            Description = recommendation.Description,
            Classroom = new ClassRoomDto
            {
                Id = recommendation.Classroom.Id,
                Description = recommendation.Classroom.Description,
                Shift = recommendation.Classroom.Shift
            }
        };
    }

    // POST: api/bookrecommendation
    [HttpPost]
    public async Task<ActionResult<BookRecommendationDto>> PostBookRecommendation(BookRecommendationDto recommendationDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recommendation = new BookRecommendation
            {
                Teacher = await _context.Teachers.FindAsync(recommendationDto.TeacherId), // Atribua o professor pela ID
                RecommendationDate = recommendationDto.RecommendationDate,
                Description = recommendationDto.Description
            };

            recommendation.Book = await _context.Books.FindAsync(recommendationDto.Classroom.Id);
            recommendation.Classroom =await _context.ClassRooms.FindAsync(recommendationDto.Classroom.Id);

            _context.BookRecommendations.Add(recommendation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookRecommendation), new { id = recommendation.Id }, recommendationDto);
        }
        catch (System.Exception)
        {
            return BadRequest("OCorreu um erro ao inserir a recomendação");
        }
    }

    // PUT: api/bookrecommendation/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBookRecommendation(int id, BookRecommendationDto recommendationDto)
    {
        var recommendation = await _context.BookRecommendations
            .Include(br => br.Teacher) // Incluindo professor para acesso
            .Include(br => br.Book) // Incluindo livros para atualização
            .Include(br => br.Classroom) // Incluindo turmas para atualização
            .FirstOrDefaultAsync(br => br.Id == id);

        if (recommendation == null)
        {
            return NotFound();
        }

        // Atualizando os dados da recomendação
        recommendation.Teacher = await _context.Teachers.FindAsync(recommendationDto.TeacherId);
        recommendation.Book = await _context.Books.FindAsync(recommendationDto.Book.Id);
        recommendation.Classroom = await _context.ClassRooms.FindAsync(recommendationDto.Classroom.Id);
        recommendation.RecommendationDate = recommendationDto.RecommendationDate;
        recommendation.Description = recommendationDto.Description;

        _context.Entry(recommendation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookRecommendationExists(id))
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

    // DELETE: api/bookrecommendation/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookRecommendation(int id)
    {
        var recommendation = await _context.BookRecommendations.FindAsync(id);
        if (recommendation == null)
        {
            return NotFound();
        }

        _context.BookRecommendations.Remove(recommendation);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookRecommendationExists(int id)
    {
        return _context.BookRecommendations.Any(e => e.Id == id);
    }
}
