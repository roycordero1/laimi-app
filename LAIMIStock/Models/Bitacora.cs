//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LAIMIStock.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bitacora
    {
        public int idAccion { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fecha { get; set; }
        public int idUsuario { get; set; }
        public int idTipoAccion { get; set; }
    }
}
