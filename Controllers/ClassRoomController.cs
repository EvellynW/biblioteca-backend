using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class ClassRoomController : ControllerBase
{
    private readonly LibraryContext _context;

    public ClassRoomController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/classroom
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassRoomDto>>> GetClassRooms()
    {
        var classrooms = await _context.ClassRooms
            .Include(cr => cr.Students)
            .Include(cr => cr.TeacherClassRooms)
            .ThenInclude(tcr => tcr.Teacher) // Inclui professores associados
            .ToListAsync();

        return classrooms.Select(cr => new ClassRoomDto
        {
            Id = cr.Id,
            Description = cr.Description,
            Shift = cr.Shift,
            Students = cr.Students.Select(s => new StudentDto
            {
                // Mapeie os campos necessários de Student para StudentDto
                id = s.Id,
                Name = s.Name,
                // Adicione outros campos conforme necessário
            }).ToList(),
            Teachers = cr.TeacherClassRooms.Select(tcr => new TeacherDto
            {
                // Mapeie os campos necessários de Teacher para TeacherDto
                Id = tcr.Teacher.Id,
                Name = tcr.Teacher.Name,
                // Adicione outros campos conforme necessário
            }).ToList()
        }).ToList();
    }

    // GET: api/classroom/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClassRoomDto>> GetClassRoom(int id)
    {
        var classroom = await _context.ClassRooms
            .Include(cr => cr.Students)
            .Include(cr => cr.TeacherClassRooms)
            .ThenInclude(tcr => tcr.Teacher)
            .FirstOrDefaultAsync(cr => cr.Id == id);

        if (classroom == null)
        {
            return NotFound();
        }

        return new ClassRoomDto
        {
            Id = classroom.Id,
            Description = classroom.Description,
            Shift = classroom.Shift,
            Students = classroom.Students.Select(s => new StudentDto
            {
                // Mapeie os campos necessários de Student para StudentDto
                id = s.Id,
                Name = s.Name,
                // Adicione outros campos conforme necessário
            }).ToList(),
            Teachers = classroom.TeacherClassRooms.Select(tcr => new TeacherDto
            {
                // Mapeie os campos necessários de Teacher para TeacherDto
                Id = tcr.Teacher.Id,
                Name = tcr.Teacher.Name,
                // Adicione outros campos conforme necessário
            }).ToList()
        };
    }

    // POST: api/classroom
    [HttpPost]
    public async Task<ActionResult<ClassRoomDto>> PostClassRoom(ClassRoomDto classroomDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var classroom = new ClassRoom
        {
            Description = classroomDto.Description,
            Shift = classroomDto.Shift,
            Students = classroomDto.Students.Select(s => new Student
            {
                // Mapeie os campos necessários de StudentDto para Student
                Id = s.id ?? 0, // Use o ID caso seja fornecido
                Name = s.Name,
                // Adicione outros campos conforme necessário
            }).ToList(),
            TeacherClassRooms = new List<TeacherClassRoom>() // Inicializa a lista de junção
        };

        _context.ClassRooms.Add(classroom);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClassRoom), new { id = classroom.Id }, classroomDto);
    }

    // PUT: api/classroom/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClassRoom(int id, ClassRoomDto classroomDto)
    {

        var classroom = await _context.ClassRooms.FirstOrDefaultAsync(cr => cr.Id == id);

        if (classroom == null)
        {
            return NotFound();
        }

        // Atualiza os dados da turma
        classroom.Description = classroomDto.Description;
        classroom.Shift = classroomDto.Shift;

        _context.Entry(classroom).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClassRoomExists(id))
            {
                return NotFound();
            }
            else
            {
                return BadRequest("Ocorreu um erro ao atualizar a turma");
            }
        }

        return NoContent();
    }

    // DELETE: api/classroom/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClassRoom(int id)
    {
        var classroom = await _context.ClassRooms.FindAsync(id);
        if (classroom == null)
        {
            return NotFound();
        }

        _context.ClassRooms.Remove(classroom);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ClassRoomExists(int id)
    {
        return _context.ClassRooms.Any(e => e.Id == id);
    }
}
