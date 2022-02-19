namespace SE214L22.Data.Entity.AppProduct
{
    public class ReceiptProduct : AppEntity
    {
        public int ProductId { get; set; }
        public int ReceiptId { get; set; }
        public int Number { get; set; }
        public int PriceIn { get; set; }

        public Product Product { get; set; }
    }
}
