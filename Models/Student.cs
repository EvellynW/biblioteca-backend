using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Student : User
{
    [Required(ErrorMessage = "A turma é obrigatória.")]
    public ClassRoom ClassRoom { get; set; } // A turma em que o aluno está matriculado

    // Construtor
    public Student(int id, string name, string cpf, string address, bool isActive, UserRole role, ClassRoom classRoom, string? registrationNumber = null, string? phone = null)
        : base(id, name, cpf, address, isActive, role, registrationNumber, phone)
    {
        ClassRoom = classRoom;
    }
}
