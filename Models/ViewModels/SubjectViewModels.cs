using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HRMath.Models
{
    public class AddSubjectModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe introducir el nombre de la asignatura.")]
        [RegularExpression(@"([A-Z][a-z0-9]*)( [A-Za-z0-9]+)*", ErrorMessage = "El nombre debe comenzar con letra mayúscula.")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres.")]
        public string Name { get; set; }
        public bool IsAnual { get; set; }
        public bool IsOptative { get; set; }
        [Required(ErrorMessage = "Debe escoger al menos una carrera.")]
        public List<int> GradesId { get; set; }
    }

    public class SetSubjectAdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
                
        [Required(ErrorMessage = "Debe escoger al menos un administrador.")]
        public List<string> AdminsId { get; set; }
    }

    public class ListSubjectsModel
    {
        public IEnumerable<ListSubject> Subjects { get; set; }
        public string Pattern { get; set; }
        public int Faculty { get; set; }
        public int Grade { get; set; }
    }

    public class ListSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsOptative { get; set; }
        public string IsAnual { get; set; }
        public List<string> Grades { get; set; }
        public List<string> Admins { get; set; }
    }
}
