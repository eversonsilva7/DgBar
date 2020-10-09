namespace BarDoDG.Application.DTOs
{
    public class ItemCompradoDTO
    {
        public int IdItemComprado { get; set; }
        public int IdItem { get; set; }
        public int IdComanda { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
