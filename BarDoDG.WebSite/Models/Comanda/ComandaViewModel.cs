using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.WebSite.Models.Comanda
{
    public class ComandaViewModel : ViewBaseModel
    {
        public int? IdCliente { get; set; }
        public string NomeCliente { get; set; }        
    }
}
