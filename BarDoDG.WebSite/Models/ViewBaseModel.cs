using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.WebSite.Models
{
    public class ViewBaseModel
    {
        /// <summary>
        /// Mensagem retorno
        /// </summary>
        public string Mensagem { get; set; }
        /// <summary>
        /// Status HTTP
        /// </summary>
        public int Status { get; set; }
    }
}
