using System.Collections.Generic;

namespace BarDoDG.Domain.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Comanda = new HashSet<Comanda>();
        }

        public int IdCliente { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Comanda> Comanda { get; set; }
    }
}
