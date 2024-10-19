using System.ComponentModel.DataAnnotations;

public class LiteraryGenre
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do gênero literário é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do gênero literário não pode exceder 100 caracteres.")]
    public string Name { get; set; }

        // Relacionamento de um-para-muitos: um aluno pode ter vários aluguéis
    public List<Book> Books { get; set; }


    // Construtor

    public LiteraryGenre()
    {
        
    }
    public LiteraryGenre(string name)
    {
        
        Name = name;
    }
}
