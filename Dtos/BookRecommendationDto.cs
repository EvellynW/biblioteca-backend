using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BookRecommendationDto
{
    public int? id { get; set; }
    [Required(ErrorMessage = "O professor é obrigatório.")]
    public int TeacherId { get; set; } // Professor que faz a recomendação

    [Required(ErrorMessage = "A lista de livros é obrigatória.")]
    public BookDto Book { get; set; } // Lista de livros recomendados

    [Required(ErrorMessage = "A data de recomendação é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime RecommendationDate { get; set; } // Data da recomendação

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Description { get; set; } // Descrição da recomendação

    [Required(ErrorMessage = "A lista de turmas é obrigatória.")]
    public ClassRoomDto Classroom { get; set; } // Lista de turmas para as quais o livro foi recomendado
}
