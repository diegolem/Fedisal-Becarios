//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fedisal_Becario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nota
    {
        public int idNota { get; set; }
        public string nombreMateria { get; set; }
        public Nullable<decimal> nota1 { get; set; }
        public Nullable<bool> cumplioTercio { get; set; }
        public Nullable<int> idCiclo { get; set; }
    
        public virtual Ciclo Ciclo { get; set; }
    }
}