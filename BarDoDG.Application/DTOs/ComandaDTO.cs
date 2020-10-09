using System;
using System.Collections.Generic;

namespace BarDoDG.Application.DTOs
{
    public class ComandaDTO
    {
        public int IdComanda { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? ValorTotalComDesconto { get; set; }
        public int? IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public List<ItemCompradoDTO> LstItemComprado { get; set; }
    }

    public class ComandaInsertDTO
    {
        public int? IdCliente { get; set; }
        public string NomeCliente { get; set; }
    }
}
