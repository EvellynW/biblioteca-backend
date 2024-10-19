using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class RentalDto
{
    public int? id { get; set; }

    [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
    public int StudentId { get; set; } // ID do aluno que está alugando o livro

    [Required(ErrorMessage = "A lista de livros é obrigatória.")]
    public BookDto Book { get; set; } // Lista de IDs dos livros alugados

    [Required(ErrorMessage = "A data de aluguel é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
    public DateTime RentalDate { get; set; } // Data em que o aluguel foi realizado

    [Required(ErrorMessage = "A data de devolução é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
    public DateTime DueDate { get; set; } // Data em que o livro deve ser devolvido

    [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
    public DateTime? ReturnDate { get; set; } // Data em que o livro foi devolvido (opcional)
}
