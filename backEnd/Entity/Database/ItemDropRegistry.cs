namespace Master.Entity.Database
{
    public class ItemDropRegistry
    {
        public long id { get; set; }
        public long? fkUser { get; set; }
        public long? fkFolder { get; set; }
        public long? fkItem { get; set; }
        public long? fkItemDrop { get; set; }
        public long? nuMonth { get; set; }
        public long? nuYear { get; set; }
        public long? nuPayment { get; set; }
        public long? vlCents { get; set; }
        public bool? bIncome { get; set; }
    }
}
