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
    
    public partial class BitacoraIncidentes
    {
        public int idBitacora { get; set; }
        public Nullable<int> idTipoIncidente { get; set; }
        public string idBecario { get; set; }
    
        public virtual Becario Becario { get; set; }
        public virtual TipoIncidente TipoIncidente { get; set; }
    }
}