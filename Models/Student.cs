using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Student : User
{
    [Required(ErrorMessage = "A turma é obrigatória.")]
    public ClassRoom ClassRoom { get; set; } // A turma em que o aluno está matriculado

    // Relacionamento de um-para-muitos: um aluno pode ter vários aluguéis
    public List<Rental> Rentals { get; set; }


    public Student()
    {

    }

    // Construtor
    public Student( string name, string cpf, string address, bool isActive, EUserRole role, ClassRoom classRoom, List<Rental> rentals, string? registrationNumber = null, string? phone = null)
        : base(name, cpf, address, isActive, role, registrationNumber, phone)
    {
        ClassRoom = classRoom;
        Rentals = rentals;
    }



}
