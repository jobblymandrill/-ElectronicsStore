
namespace ElecronicsStore.DB.Models.DataModels
{
    public class Filial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsForeign { get; set; }
    }
}
