using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.WebSite.Models.Comanda
{
    public class ComandasAtivasViewModel : ViewBaseModel
    {
        public List<ComandaDTO> LstComandaAtiva { get; set; }

        public ComandasAtivasViewModel()
        {
            LstComandaAtiva = new List<ComandaDTO>();
        }
    }
}
