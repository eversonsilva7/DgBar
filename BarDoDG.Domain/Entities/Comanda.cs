using System;
using System.Collections.Generic;

namespace BarDoDG.Domain.Entities
{
    public partial class Comanda
    {
        public Comanda()
        {
            ItemComprado = new HashSet<ItemComprado>();
        }

        public int IdComanda { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? ValorTotalComDesconto { get; set; }
        public int IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<ItemComprado> ItemComprado { get; set; }
    }
}
