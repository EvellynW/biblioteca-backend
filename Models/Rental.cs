using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Rental
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; } // Identificador único para o aluguel

    [Required(ErrorMessage = "O aluno é obrigatório.")]
    public Student Student { get; set; } // Aluno que está alugando o livro

    [Required(ErrorMessage = "A lista de livros é obrigatória.")]
    public List<Book> Books { get; set; } // Lista de livros alugados

    [Required(ErrorMessage = "A data de aluguel é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime RentalDate { get; set; } // Data em que o aluguel foi realizado

    [Required(ErrorMessage = "A data de devolução é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; } // Data em que o livro deve ser devolvido

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; } // Data em que o livro foi devolvido (opcional)

    public bool IsReturned { get; set; } // Indica se o livro foi devolvido

    // Construtor
    public Rental(int id, Student student, List<Book> books, DateTime rentalDate, DateTime dueDate)
    {
        Id = id;
        Student = student;
        Books = books;
        RentalDate = rentalDate;
        DueDate = dueDate;
        ReturnDate = null; // Inicialmente, o livro não foi devolvido
        IsReturned = false; // Inicialmente, o livro não foi devolvido
    }

    // Método para marcar o aluguel como devolvido
    public void ReturnBooks(DateTime returnDate)
    {
        ReturnDate = returnDate;
        IsReturned = true;
    }
}
