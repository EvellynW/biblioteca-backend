using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Teacher : User
{
    
    public List<TeacherClassRoom> TeacherClassRooms { get; set; } // Navegação para a tabela de junção

    // Construtor
    public Teacher(string name, string cpf, string address, bool isActive, EUserRole role, List<TeacherClassRoom> teacherClassRooms, string? registrationNumber = null, string? phone = null)
        : base( name, cpf, address, isActive, role, registrationNumber, phone)
    {
        TeacherClassRooms = teacherClassRooms;
    }
    public Teacher()
    {
        
    }
}

