using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace HRMath.Models
{

    public class ListProfessorsModel 
    {
        public string ScientificGrade { get; set; }
        public string TeachingCategory { get; set; }
        public string Pattern { get; set; }
        public string State { get; set; }
        public int? SelectedSubject { get; set; }
        public IEnumerable<Professor> Professors { get; set; }
    }

    public class RegisterProfessorModel
    {

        [Required(ErrorMessage="Debe ingresar su número de carnet de identidad")]
        [StringLength(11,ErrorMessage="El carnet de identidad debe contener 11 dígitos", MinimumLength=11 )]
        [RegularExpression(@"[0-9]+", ErrorMessage="El carnet de identidad debe contener solo dígitos")]
        public string PersonalId {get; set;}


        [Required(ErrorMessage="Debe ingresar su nombre")]
        [RegularExpression(@"([A-Z][a-z]+)( [A-Z]?[a-z]+)*", ErrorMessage="El nombre debe comenzar con letra mayúscula y no debe contener números ni símbolos")]
        public string Name {get; set;}

        [Required(ErrorMessage="Por favor ingrese el primer appellido")]
        [RegularExpression(@"([A-Z][a-z]+)( [A-Z]?[a-z]+)*", ErrorMessage="Los apellidos deben comenzar con letra mayúscula y no deben contener números ni símbolos")]
        public string FirstLastName {get; set;}

        [Required(ErrorMessage="Por favor ingrese el segundo appellido")]
        [RegularExpression(@"([A-Z][a-z]+)( [A-Z]?[a-z]+)*", ErrorMessage="Los apellidos deben comenzar con letra mayúscula y no deben contener números ni símbolos")]
        public string SecondLastName {get; set;}


        [Required(ErrorMessage="Debe ingresar su correo electrónico")]
        [EmailAddress(ErrorMessage="Debe ingresar una dirección de correo válida")]
        public string Email { get; set; }


        [RegularExpression(@"[0-9]+", ErrorMessage="El número telefónico debe contener solo dígitos")]
        [StringLength(7,ErrorMessage="Los números telefónicos deben contener 7 dígitos", MinimumLength=7)]
        public string Cellphone { get; set; }


        [RegularExpression(@"[0-9]+", ErrorMessage="El número telefónico debe contener solo dígitos")]
        [StringLength(7,ErrorMessage="Los números telefónicos deben contener 7 dígitos", MinimumLength=7)]
        public string Landphone { get; set; }


        [Required(ErrorMessage="Debe ingresar su dirección particular")]
        public string Address { get; set; }


        [Required(ErrorMessage="Debe ingresar el grado científico")]
        public string ScientificGrade { get; set; }


        [Required(ErrorMessage="Debe ingresar la categoría docente")]
        public string TeachingCategory { get; set; }
        
      
    }

    public class ProfessorDetailsModel
    {

        [Required(ErrorMessage="Debe ingresar su número de carnet de identidad")]
        [StringLength(11,ErrorMessage="El carnet de identidad debe contener 11 dígitos", MinimumLength=11 )]
        [RegularExpression(@"[0-9]+", ErrorMessage="El carnet de identidad debe contener solo dígitos")]
        public string PersonalId {get; set;}


        [Required(ErrorMessage="Debe ingresar su nombre")]
        [RegularExpression(@"([A-Z][a-z]+)( [A-Z]?[a-z]+)*", ErrorMessage="El nombre debe comenzar con letra mayúsculano debe contener números ni símbolos")]
        public string Name {get; set;}

        [Required(ErrorMessage="Por favor ingrese el primer appellido")]
        [RegularExpression(@"([A-Z][a-z]+)( [A-Z]?[a-z]+)*", ErrorMessage="Los apellidos deben comenzar con letra mayúscula y no deben contener números ni símbolos")]
        public string FirstLastName {get; set;}

        [Required(ErrorMessage="Por favor ingrese el segundo appellido")]
        [RegularExpression(@"([A-Z][a-z]+)( [A-Z]?[a-z]+)*", ErrorMessage="Los apellidos deben comenzar con letra mayúscula y no deben contener números ni símbolos")]
        public string SecondLastName {get; set;}


        [Required(ErrorMessage="Debe ingresar su correo electrónico")]
        [EmailAddress(ErrorMessage="Debe ingresar una dirección de correo válida")]
        public string Email { get; set; }


        [RegularExpression(@"[0-9]+", ErrorMessage="El número telefónico debe contener solo dígitos")]
        [StringLength(7,ErrorMessage="Los números telefónicos deben contener 7 dígitos", MinimumLength=7)]
        public string Cellphone { get; set; }


        [RegularExpression(@"[0-9]+", ErrorMessage="El número telefónico debe contener solo dígitos")]
        [StringLength(7,ErrorMessage="Los números telefónicos deben contener 7 dígitos", MinimumLength=7)]
        public string Landphone { get; set; }


        [Required(ErrorMessage="Debe ingresar su dirección particular")]
        public string Address { get; set; }


        [Required(ErrorMessage="Debe ingresar el grado científico")]
        public string ScientificGrade { get; set; }


        [Required(ErrorMessage="Debe ingresar la categoría docente")]
        public string TeachingCategory { get; set; }

        public IEnumerable<Contract> Contracts {get; set;}

        public IEnumerable<TeachSubject> Subjects  {get; set;}

    }

    public class ContractProfessorModel
    {
        public Guid Professor {get; set;}
        

        [Required(ErrorMessage="Debe ingresar la fecha de inicio del contrato")]
        public DateTime StartDate {get; set;}


        public DateTime? EndDate {get; set;}


        [Required(ErrorMessage="Por favor ingrese el cargo del profesor")]
        public string Role {get; set;}


        [Required(ErrorMessage="Por favor ingrese el tipo de contrato")]
        public string Type {get; set;}
    }

}