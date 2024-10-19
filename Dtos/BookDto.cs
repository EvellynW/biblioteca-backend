using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BookDto
{
    public int? Id { get; set; } // Identificador único do livro

    [Required(ErrorMessage = "A editora é obrigatória.")]
    [StringLength(100, ErrorMessage = "A editora não pode exceder 100 caracteres.")]
    public string Publisher { get; set; } // Editora do livro

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(200, ErrorMessage = "O título não pode exceder 200 caracteres.")]
    public string Title { get; set; } // Título do livro

    public string? ISBN { get; set; } // ISBN (opcional)

    [Required(ErrorMessage = "O escritor é obrigatório.")]
    public List<string> Authors { get; set; } // Lista de escritores

    [Range(1000, 9999, ErrorMessage = "O ano de publicação deve estar entre 1000 e 9999.")]
    public int PublicationYear { get; set; } // Ano de publicação

    [StringLength(1000, ErrorMessage = "O resumo não pode exceder 1000 caracteres.")]
    public string Summary { get; set; } // Resumo do livro

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
    public int Quantity { get; set; } = 1; // Quantidade disponível (valor padrão: 1)
    public LiteraryGenreDto literaryGenre { get; set; }

}
