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
    
    public partial class selectSuministros_Result
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaIngreso { get; set; }
        public Nullable<System.DateTime> fechaCaducidad { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<int> objetoGasto { get; set; }
        public string localizacion { get; set; }
        public int cantidad { get; set; }
        public int idSuministro { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public string nombre { get; set; }
    }
}