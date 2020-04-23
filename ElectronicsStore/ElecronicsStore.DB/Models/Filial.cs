namespace ElecronicsStore.DB.Models
{
    public class Filial
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public Country Country { get; set; }
        public bool IsForeign { get; set; }
    }
}
