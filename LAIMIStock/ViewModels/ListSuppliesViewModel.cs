using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAIMIStock.Models;

namespace LAIMIStock.ViewModels
{
    public class ListSuppliesViewModel
    {
        public List<Suministros> Suministros { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}