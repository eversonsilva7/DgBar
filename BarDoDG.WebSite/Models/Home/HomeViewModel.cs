using BarDoDG.WebSite.Models.Comanda;

namespace BarDoDG.WebSite.Models.Home
{
    public class HomeViewModel : ViewBaseModel
    {
        public int QuantidadeItensComprados { get; set; }
        public int QuantidadeClientes { get; set; }
        public int QuantidadeComandas { get; set; }
        public decimal TotalComandas { get; set; }        
        public ComandasAtivasViewModel ComandasAtivasViewModel { get; set; }

        public HomeViewModel()
        {
            ComandasAtivasViewModel = new ComandasAtivasViewModel();
        }
    }
}
