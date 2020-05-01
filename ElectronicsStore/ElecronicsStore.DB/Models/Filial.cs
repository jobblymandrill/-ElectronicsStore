namespace ElecronicsStore.DB.Models
{
    public class Filial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public bool? IsForeign { get; set; }
    }
}
