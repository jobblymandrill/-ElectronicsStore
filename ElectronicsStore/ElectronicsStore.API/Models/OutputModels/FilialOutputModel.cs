using ElecronicsStore.DB.Models;

namespace ElectronicsStore.API.Models.OutputModels
{
    public class FilialOutputModel
    {
         public int Id { get; set; }
        public int Name { get; set; }
        public Country Country { get; set; }
        public bool IsForeign { get; set; }
    }
}
