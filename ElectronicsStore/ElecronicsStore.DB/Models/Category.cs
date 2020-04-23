using System;

namespace ElecronicsStore.DB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SubcategoryId { get; set; }

        public object Clone()
        {
            return new Category()
            {
                Id = this.Id,
                Name = this.Name,
                SubcategoryId = this.SubcategoryId
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Category categoryInputModel &&
                   Id == categoryInputModel.Id &&
                   Name == categoryInputModel.Name &&
                   SubcategoryId == categoryInputModel.SubcategoryId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, SubcategoryId);
        }
    }
}
