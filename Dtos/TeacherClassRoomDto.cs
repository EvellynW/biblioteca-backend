using System.ComponentModel.DataAnnotations;

public class TeacherClassRoomDto
{
    public int? Id { get; set; } // Identificador único da junção entre professor e turma

    [Required(ErrorMessage = "O ID do professor é obrigatório.")]
    public int TeacherId { get; set; } // ID do professor

    [Required(ErrorMessage = "O ID da turma é obrigatório.")]
    public int ClassRoomId { get; set; } // ID da turma
}
