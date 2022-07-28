namespace Master.Entity.Domain.Entries
{
    public class DtoEntriesItemDropListItem
    {
        public long id { get; set; }

        public string date_register { get; set; }

        public long cents { get; set; }

        public long installments { get; set; }

        public bool bIncome { get; set; }
    }
}
