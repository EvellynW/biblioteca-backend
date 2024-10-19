using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BookRecommendation
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; } // Identificador único para a recomendação de livro

    [Required(ErrorMessage = "O professor é obrigatório.")]
    public Teacher Teacher { get; set; } // Professor que faz a recomendação

    [Required(ErrorMessage = "O livro é obrigatório.")]
    public Book Book { get; set; } // Lista de livros recomendados

    [Required(ErrorMessage = "A data de recomendação é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime RecommendationDate { get; set; } // Data em que o livro foi recomendado

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Description { get; set; } // Descrição da recomendação

    [Required(ErrorMessage = "Turma é obrigatória.")]
    public ClassRoom Classroom { get; set; } // Lista de turmas para as quais o livro foi recomendado

    // Construtor

    public BookRecommendation()
    {

    }
    
    public BookRecommendation(Teacher teacher, List<Book> books, DateTime recommendationDate, 
        string description, ClassRoom classroom, Book book)
    {
       
        Teacher = teacher;
        Book = book;
        RecommendationDate = recommendationDate;
        Description = description;
        Classroom = classroom;
    }
    
}
