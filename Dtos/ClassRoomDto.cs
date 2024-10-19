using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ClassRoomDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "A descrição da turma é obrigatória.")]
    [StringLength(100, ErrorMessage = "A descrição da turma não pode exceder 100 caracteres.")]
    public string Description { get; set; } // Descrição da turma

    [Required(ErrorMessage = "O turno é obrigatório.")]
    [StringLength(50, ErrorMessage = "O turno não pode exceder 50 caracteres.")]
    public string Shift { get; set; } // Turno da turma

    [JsonIgnoreAttribute]
    public List<StudentDto> Students { get; set; } // Lista de alunos que pertencem à turma

    [JsonIgnoreAttribute]
    public List<TeacherDto> Teachers { get; set; } // Lista de professores que lecionam para essa turma

    // Construtor sem parâmetros
    public ClassRoomDto() 
    {
        Students = new List<StudentDto>(); // Inicializa a lista de alunos
        Teachers = new List<TeacherDto>(); // Inicializa a lista de professores
    }

}
