using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fedisal_Becario.Models
{
    public class BecarioModels
    {

    }
    public class RegisterNoteModel{
        public int idCiclo { get; set; }
        public string idBecario { get; set; }
        [Required]
        [Display(Name = "Año")]
        [RegularExpression("^20[0-9][0-9]$", ErrorMessage = "Ingrese una fecha válida")]
        public int anio { get; set; }
        [Required]
        [Display(Name = "Numero de ciclo")]
        [Range(minimum: 1, maximum: 4, ErrorMessage = "Ingrese un ciclo válido")]
        [RegularExpression("^[1-4]$", ErrorMessage = "Ingrese un ciclo valido")]
        public int nCiclo { get; set; }
        [Required]
        [Display(Name = "Nombre de la materia")]
        [RegularExpression("^([A-Z]|[a-z]|[ñÑ]|[áéíóúÁÉÍÓÚ]){1}[a-zA-Z1-4 áéíóúñÑ]$", ErrorMessage = "Ingrese un nombre válido")]
        public string nombreMateria { get; set; }
        [Required]
        [Display(Name = "Nota")]
        [RegularExpression("^(10.0|[0-9].[0-9][0-9]|[0-9]|10|[0-9].[0-9])$", ErrorMessage = "Ingrese una nota válida")]
        public decimal nota { get; set; }
        [Required]
        [Display(Name = "Tercio superior")]
        public bool cumplioTercio { get; set; }
        }
    }