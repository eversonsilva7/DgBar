using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.WebSite.Models.Comanda
{
    public class HistoricoComandasViewModel
    {
        public List<ComandaDTO> LstHistoricoComanda { get; set; }

        public HistoricoComandasViewModel()
        {
            LstHistoricoComanda = new List<ComandaDTO>();
        }
    }
}
