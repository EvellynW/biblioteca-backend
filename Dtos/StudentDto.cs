using System.ComponentModel.DataAnnotations;

public class StudentDto
{
    public int? id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string CPF { get; set; }

    [StringLength(50, ErrorMessage = "O número de matrícula não pode exceder 50 caracteres.")]
    public string? RegistrationNumber { get; set; }

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public string Address { get; set; }

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "O status de ativo é obrigatório.")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "O papel é obrigatório.")]
    public EUserRole Role { get; set; }

    [Required(ErrorMessage = "A turma é obrigatória.")]
    public ClassRoomDto ClassRoom { get; set; } // A turma em que o aluno está matriculado
}
