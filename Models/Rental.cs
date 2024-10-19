using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Rental
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; } // Identificador único para o aluguel
    public int StudentId { get; set; } // Identificador único para o aluguel

    public int BookId { get; set; } // Identificador único para o aluguel


    [Required(ErrorMessage = "O aluno é obrigatório.")]
    public Student Student { get; set; } // Aluno que está alugando o livro

    [Required(ErrorMessage = "A lista de livros é obrigatória.")]
    public Book Book { get; set; } // Lista de livros alugados

    [Required(ErrorMessage = "A data de aluguel é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime RentalDate { get; set; } // Data em que o aluguel foi realizado

    [Required(ErrorMessage = "A data de devolução é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; } // Data em que o livro deve ser devolvido

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; } // Data em que o livro foi devolvido (opcional)


    // Construtor
    public Rental(Student student, Book books, DateTime rentalDate, DateTime dueDate)
    {

        Student = student;
        Book = books;
        RentalDate = rentalDate;
        DueDate = dueDate;
        ReturnDate = null; // Inicialmente, o livro não foi devolvido
    }

    // Método para marcar o aluguel como devolvido
    public void ReturnBooks(DateTime returnDate)
    {
        ReturnDate = returnDate;
    }

    public Rental()
    {

    }
}
