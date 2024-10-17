using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BookRecommendation
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; } // Identificador único para a recomendação de livro

    [Required(ErrorMessage = "O professor é obrigatório.")]
    public Teacher Teacher { get; set; } // Professor que faz a recomendação

    [Required(ErrorMessage = "A lista de livros é obrigatória.")]
    public List<Book> Books { get; set; } // Lista de livros recomendados

    [Required(ErrorMessage = "A data de recomendação é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime RecommendationDate { get; set; } // Data em que o livro foi recomendado

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Description { get; set; } // Descrição da recomendação

    [Required(ErrorMessage = "A lista de turmas é obrigatória.")]
    public List<ClassRoom> Classrooms { get; set; } // Lista de turmas para as quais o livro foi recomendado

    // Construtor
    public BookRecommendation(int id, Teacher teacher, List<Book> books, DateTime recommendationDate, string description, List<ClassRoom> classrooms)
    {
        Id = id;
        Teacher = teacher;
        Books = books;
        RecommendationDate = recommendationDate;
        Description = description;
        Classrooms = classrooms;
    }
}
