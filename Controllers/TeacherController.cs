using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly LibraryContext _context;

    public TeacherController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/Teacher
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
    {
        var teachers = await _context.Teachers
            .Select(t => new TeacherDto
            {
                Id = t.Id,
                Name = t.Name,
                CPF = t.CPF,
                RegistrationNumber = t.RegistrationNumber,
                Address = t.Address,
                Phone = t.Phone,
                IsActive = t.IsActive,
                Role = t.Role,
                ClassRoomsDto = t.TeacherClassRooms.Where(x => x.TeacherId.Equals(t.Id))
                .Select(c => new ClassRoomDto{
                    Id = c.ClassRoom.Id,
                    Description = c.ClassRoom.Description,
                    Shift = c.ClassRoom.Shift
                })
                .ToList() // Obtém as turmas
            })
            .ToListAsync();

        return Ok(teachers);
    }

    // GET: api/Teacher/id
    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
    {
        var teacher = await _context.Teachers
            .Where(t => t.Id == id)
            .Select(t => new TeacherDto
            {
                Id = t.Id,
                Name = t.Name,
                CPF = t.CPF,
                RegistrationNumber = t.RegistrationNumber,
                Address = t.Address,
                Phone = t.Phone,
                IsActive = t.IsActive,
                Role = t.Role,
                 ClassRoomsDto = t.TeacherClassRooms.Where(x => x.TeacherId.Equals(t.Id))
                .Select(c => new ClassRoomDto{
                    Id = c.ClassRoom.Id,
                    Description = c.ClassRoom.Description,
                    Shift = c.ClassRoom.Shift
                })
                .ToList() // Obtém as turmas
            })
            .FirstOrDefaultAsync();

        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }

    // POST: api/Teacher
    [HttpPost]
    public async Task<ActionResult<TeacherDto>> PostTeacher(TeacherDto teacherDto)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = new Teacher
            {
                Name = teacherDto.Name,
                CPF = teacherDto.CPF,
                RegistrationNumber = teacherDto.RegistrationNumber,
                Address = teacherDto.Address,
                Phone = teacherDto.Phone,
                IsActive = teacherDto.IsActive,
                Role = teacherDto.Role
                // Note: TeacherClassRooms será preenchido após a criação
            };
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            // Associa as turmas ao professor após a criação
            if (teacherDto.ClassRoomIds != null)
            {
                foreach (var classRoomId in teacherDto.ClassRoomIds)
                {
                    if (classRoomId != 0)
                    {
                        var teacherClassRoom = new TeacherClassRoom
                        {
                            TeacherId = teacher.Id,
                            ClassRoomId = classRoomId
                        };
                       await _context.TeacherClassRooms.AddAsync(teacherClassRoom);
                        await _context.SaveChangesAsync();

                    }
                }
            }

            teacherDto.Id = teacher.Id;

            return CreatedAtAction(nameof(GetTeacher), new { id = teacherDto.Id }, teacherDto);

        }
        catch (System.Exception e)
        {

            return BadRequest("erro ao cadastrar professor" + e.GetBaseException().Message);
        }

    }

    // PUT: api/Teacher/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTeacher(int id, TeacherDto teacherDto)
    {
        if (id != teacherDto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        teacher.Name = teacherDto.Name;
        teacher.CPF = teacherDto.CPF;
        teacher.RegistrationNumber = teacherDto.RegistrationNumber;
        teacher.Address = teacherDto.Address;
        teacher.Phone = teacherDto.Phone;
        teacher.IsActive = teacherDto.IsActive;

        _context.Entry(teacher).State = EntityState.Modified;

        // Atualiza as associações de turmas
        var currentClassRoomIds = await _context.TeacherClassRooms
            .Where(tc => tc.TeacherId == id)
            .Select(tc => tc.ClassRoomId)
            .ToListAsync();

        // Remove associações antigas
        foreach (var classRoomId in currentClassRoomIds)
        {
            if (!teacherDto.ClassRoomIds.Contains(classRoomId))
            {
                var teacherClassRoom = await _context.TeacherClassRooms
                    .FirstOrDefaultAsync(tc => tc.TeacherId == id && tc.ClassRoomId == classRoomId);
                if (teacherClassRoom != null)
                {
                    _context.TeacherClassRooms.Remove(teacherClassRoom);
                }
            }
        }

        // Adiciona novas associações
        foreach (var classRoomId in teacherDto.ClassRoomIds)
        {
            if (!currentClassRoomIds.Contains(classRoomId))
            {
                var teacherClassRoom = new TeacherClassRoom
                {
                    TeacherId = teacher.Id,
                    ClassRoomId = classRoomId
                };
                _context.TeacherClassRooms.Add(teacherClassRoom);
            }
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Teacher/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
