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
    
    public partial class Desembolso
    {
        public int idDesembolso { get; set; }
        public Nullable<decimal> monto { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idTipoDesembolso { get; set; }
        public int idCiclo { get; set; }
    
        public virtual Ciclo Ciclo { get; set; }
        public virtual TipoDesembolso TipoDesembolso { get; set; }
    }
}
