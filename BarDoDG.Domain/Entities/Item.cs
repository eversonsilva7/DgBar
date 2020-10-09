using System;
using System.Collections.Generic;

namespace BarDoDG.Domain.Entities
{
    public partial class Item
    {
        public Item()
        {
            ItemComprado = new HashSet<ItemComprado>();
        }

        public int IdItem { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<ItemComprado> ItemComprado { get; set; }
    }
}
