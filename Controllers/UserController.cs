using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly LibraryContext _context;

    public UserController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users
            .Select(u => new UserDto
            {
                id = u.Id,
                Name = u.Name,
                CPF = u.CPF,
                RegistrationNumber = u.RegistrationNumber,
                Address = u.Address,
                Phone = u.Phone,
                IsActive = u.IsActive,
                Role = u.Role
            })
            .ToListAsync();

        return Ok(users);
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserDto
            {
                id = u.Id,
                Name = u.Name,
                CPF = u.CPF,
                RegistrationNumber = u.RegistrationNumber,
                Address = u.Address,
                Phone = u.Phone,
                IsActive = u.IsActive,
                Role = u.Role
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST: api/User

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
    {


        if (!ModelState.IsValid)
        {
            Console.WriteLine(ModelState);
            return BadRequest(ModelState);
        }

        var user = new User
        {
            Name = userDto.Name,
            CPF = userDto.CPF,
            RegistrationNumber = userDto.RegistrationNumber,
            Address = userDto.Address,
            Phone = userDto.Phone,
            IsActive = userDto.IsActive,
            Role = userDto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        userDto.id = user.Id;

        return CreatedAtAction(nameof(GetUser), new { id = userDto.id }, userDto);
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Name = userDto.Name;
        user.CPF = userDto.CPF;
        user.RegistrationNumber = userDto.RegistrationNumber;
        user.Address = userDto.Address;
        user.Phone = userDto.Phone;
        user.IsActive = userDto.IsActive;
        user.Role = userDto.Role;

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
