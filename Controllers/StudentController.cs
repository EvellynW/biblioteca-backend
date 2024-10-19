using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly LibraryContext _context;

    public StudentController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
    {
        var students = await _context.Students
            .Select(s => new StudentDto
            {
                id = s.Id,
                Name = s.Name,
                CPF = s.CPF,
                RegistrationNumber = s.RegistrationNumber,
                Address = s.Address,
                Phone = s.Phone,
                IsActive = s.IsActive,
                Role = s.Role,
                ClassRoom = new ClassRoomDto
                {
                    Id = s.ClassRoom.Id,
                    Description = s.ClassRoom.Description,
                    Shift = s.ClassRoom.Shift
                }
            })
            .ToListAsync();

        return Ok(students);
    }

    // GET: api/Student/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        var student = await _context.Students
            .Where(s => s.Id == id)
            .Select(s => new StudentDto
            {
                id = s.Id,
                Name = s.Name,
                CPF = s.CPF,
                RegistrationNumber = s.RegistrationNumber,
                Address = s.Address,
                Phone = s.Phone,
                IsActive = s.IsActive,
                Role = s.Role,
                ClassRoom = new ClassRoomDto
                {
                    Id = s.ClassRoom.Id,
                    Description = s.ClassRoom.Description,
                    Shift = s.ClassRoom.Shift
                }
            })
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    // POST: api/Student
    [HttpPost]
    public async Task<ActionResult<StudentDto>> PostStudent(StudentDto studentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = new Student
        {
            Name = studentDto.Name,
            CPF = studentDto.CPF,
            RegistrationNumber = studentDto.RegistrationNumber,
            Address = studentDto.Address,
            Phone = studentDto.Phone,
            IsActive = studentDto.IsActive,
            Role = studentDto.Role,
            ClassRoom = await _context.ClassRooms.FirstOrDefaultAsync(r => r.Id == studentDto.ClassRoom.Id)
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        studentDto.id = student.Id;

        return CreatedAtAction(nameof(GetStudent), new { id = studentDto.id }, studentDto);
    }

    // PUT: api/Student/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, StudentDto studentDto)
    {
        if (id != studentDto.id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        student.Name = studentDto.Name;
        student.CPF = studentDto.CPF;
        student.RegistrationNumber = studentDto.RegistrationNumber;
        student.Address = studentDto.Address;
        student.Phone = studentDto.Phone;
        student.IsActive = studentDto.IsActive;
        student.Role = studentDto.Role;
        student.ClassRoom = await _context.ClassRooms.FirstOrDefaultAsync(r => r.Id == studentDto.ClassRoom.Id); // Assegure-se de que ClassRoom.id não é nulo

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Student/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
