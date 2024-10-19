using System.ComponentModel.DataAnnotations;

public class UserDto
{
    public int? id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public string Name { get; set; } // Nome do usuário

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string CPF { get; set; } // CPF do usuário

    [StringLength(50, ErrorMessage = "O número de matrícula não pode exceder 50 caracteres.")]
    public string? RegistrationNumber { get; set; } // Número de matrícula (opcional)

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public string Address { get; set; } // Endereço do usuário

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    public string? Phone { get; set; } // Telefone (opcional)

    [Required(ErrorMessage = "O status de ativo é obrigatório.")]
    public bool IsActive { get; set; } // Status de ativo do usuário

    [Required(ErrorMessage = "O papel é obrigatório.")]
    public EUserRole Role { get; set; } // Papel do usuário
}
