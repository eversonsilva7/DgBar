namespace BarDoDG.Domain.Entities
{
    public partial class ItemComprado
    {
        public int IdItemComprado { get; set; }
        public int IdItem { get; set; }
        public int IdComanda { get; set; }

        public virtual Comanda IdComandaNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}
