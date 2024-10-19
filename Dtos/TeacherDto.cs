using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TeacherDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public string Name { get; set; } // Nome do professor

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string CPF { get; set; } // CPF do professor

    [StringLength(50, ErrorMessage = "O número de matrícula não pode exceder 50 caracteres.")]
    public string? RegistrationNumber { get; set; } // Número de matrícula (opcional)

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public string Address { get; set; } // Endereço do professor

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    public string? Phone { get; set; } // Telefone (opcional)

    [Required(ErrorMessage = "O status de ativo é obrigatório.")]
    public bool IsActive { get; set; } // Status de ativo do professor

    [Required(ErrorMessage = "O papel é obrigatório.")]
    public EUserRole Role { get; set; } // Papel do professor

    public List<int> ClassRoomIds { get; set; } // Lista de identificadores das turmas em que o professor leciona

    public List<ClassRoomDto> ClassRoomsDto{ get; set; }

    public TeacherDto(int? id, string name, string phone, bool isActive, List<int> classRoomIds)
    {
        Id = id ?? 0;
        Name = name ?? string.Empty;
        Phone = phone ?? string.Empty;
        IsActive = isActive;
        Role = EUserRole.Teacher; 
        ClassRoomIds = classRoomIds ?? new List<int>();
    }

    public TeacherDto(int? id, string name, string phone, bool isActive, EUserRole role,List<ClassRoomDto> classRoomsDto)
    {
        Id = id ?? 0;
        Name = name ?? string.Empty;
        Phone = phone ?? string.Empty;
        IsActive = isActive;
        Role = EUserRole.Teacher; 
        classRoomsDto = new List<ClassRoomDto>();
    }

    public TeacherDto()
    {
        Role = EUserRole.Teacher;
        ClassRoomsDto = new List<ClassRoomDto>();
    }

}


