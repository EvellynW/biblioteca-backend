using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ClassRoom
{
    [Key] // Indica que esta propriedade é a chave primária
    public int Id { get; set; } // Identificador único para a turma

    [Required(ErrorMessage = "A descrição da turma é obrigatória.")]
    [StringLength(100, ErrorMessage = "A descrição da turma não pode exceder 100 caracteres.")]
    public string Description { get; set; } // Descrição da turma

    [Required(ErrorMessage = "O turno é obrigatório.")]
    [StringLength(50, ErrorMessage = "O turno não pode exceder 50 caracteres.")]
    public string Shift { get; set; } // Turno da turma

    public List<Student> Students { get; set; } // Lista de alunos que pertencem à turma

    public List<TeacherClassRoom> TeacherClassRooms { get; set; } // Lista de junção para professores (muitos-para-muitos)



    public ClassRoom()
    {
        Students = new List<Student>(); // Inicializa a lista de alunos
        TeacherClassRooms = new List<TeacherClassRoom>(); // Inicializa a lista de junção
    }

    // Construtor
    public ClassRoom( string description, string shift)
    {
       
        Description = description;
        Shift = shift;
        Students = new List<Student>(); // Inicializa a lista de alunos
        TeacherClassRooms = new List<TeacherClassRoom>(); // Inicializa a lista de junção
    }
}
