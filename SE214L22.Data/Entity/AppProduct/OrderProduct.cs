namespace SE214L22.Data.Entity.AppProduct
{
    public class OrderProduct : AppEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Number { get; set; }


        public Product Product { get; set; }
    }
}
