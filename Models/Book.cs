using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; }

    [Required(ErrorMessage = "A editora é obrigatória.")]
    [StringLength(100, ErrorMessage = "A editora não pode exceder 100 caracteres.")]
    public string Publisher { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(200, ErrorMessage = "O título não pode exceder 200 caracteres.")]
    public string Title { get; set; }

    public string? ISBN { get; set; } // ISBN não é mais obrigatório

    [Required(ErrorMessage = "O escritor é obrigatório.")]
    public List<string> Authors { get; set; } // Lista de escritores

    [Range(1000, 9999, ErrorMessage = "O ano de publicação deve estar entre 1000 e 9999.")]
    public int PublicationYear { get; set; }

    [StringLength(1000, ErrorMessage = "O resumo não pode exceder 1000 caracteres.")]
    public string Summary { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
    public int Quantity { get; set; } = 1; // Novo atributo para quantidade

    public LiteraryGenre LiteraryGenre { get; set; }
    public int LiteraryGenreId { get; set; } // Identificador único para o genero Literário

    public List<Rental> Rentals { get; set; }
    // Construtor sem parâmetros
    public Book() { }

    // Construtor com parâmetros
    public Book(string publisher, LiteraryGenre genre, string title, List<string> authors, int publicationYear, string summary, int quantity, string? isbn = null)
    {
        LiteraryGenre = genre;
        Publisher = publisher;
        Title = title;
        Authors = authors;
        PublicationYear = publicationYear;
        Summary = summary;
        Quantity = quantity;
        ISBN = isbn; // ISBN é opcional
    }
}
