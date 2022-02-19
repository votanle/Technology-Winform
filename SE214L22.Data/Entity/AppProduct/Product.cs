namespace SE214L22.Data.Entity.AppProduct
{
    public class Product : AppEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int Number { get; set; }
        public int PriceIn { get; set; }
        public int PriceOut { get; set; }
        public int? WarrantyPeriod { get; set; }
        public float? ReturnRate { get; set; }
        public int Status { get; set; }
        public string Photo { get; set; }
        public int isDelete { get; set; }


        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }

    public enum ProductStatus
    {
        Available,
        Suspended,
    }
}
