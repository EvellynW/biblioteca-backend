using System.ComponentModel.DataAnnotations;

public class ClassRoom
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; } // Identificador único para a turma

    [Required(ErrorMessage = "A descrição da turma é obrigatória.")]
    [StringLength(100, ErrorMessage = "A descrição da turma não pode exceder 100 caracteres.")]
    public string Description { get; set; } // Descrição da turma

    [Required(ErrorMessage = "O turno é obrigatório.")]
    [StringLength(50, ErrorMessage = "O turno não pode exceder 50 caracteres.")]
    public string Shift { get; set; } // Turno da turma

    // Construtor
    public ClassRoom(int id, string description, string shift)
    {
        Id = id;
        Description = description;
        Shift = shift;
    }
}
