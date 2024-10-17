using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Teacher : User
{
    [Required(ErrorMessage = "A lista de turmas é obrigatória.")]
    public List<ClassRoom> ClassRooms { get; set; } // Lista de turmas em que o professor leciona

    // Construtor
    public Teacher(int id, string name, string cpf, string address, bool isActive, UserRole role, List<ClassRoom> classRooms, string? registrationNumber = null, string? phone = null)
        : base(id, name, cpf, address, isActive, role, registrationNumber, phone)
    {
        ClassRooms = classRooms;
    }
}
