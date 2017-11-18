using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAIMIStock.Models;

namespace LAIMIStock.ViewModels
{
    public class ListAssetsViewModel
    {
        public List<Activos> Activos { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}