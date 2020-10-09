using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.WebSite.Models.Comanda
{
    public class DetalhesComandaViewModel : ViewBaseModel
    {
        public ComandaDTO ComandaDTO { get; set; }
        public List<ItemDTO> LstItem { get; set; }
        public DetalhesComandaViewModel()
        {
            ComandaDTO = new ComandaDTO();
            LstItem = new List<ItemDTO>();
        }
    }
}
