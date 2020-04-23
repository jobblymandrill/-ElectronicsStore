namespace ElectronicsStore.API.Models.InputModels
{
    public class Product_FilialInputModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FilialId { get; set; }
        public int Amount { get; set; }
    }
}
