using System.ComponentModel.DataAnnotations;

public class LiteraryGenreDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "O nome do gênero literário é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do gênero literário não pode exceder 100 caracteres.")]
    public string Name { get; set; } // Nome do gênero literário
}
