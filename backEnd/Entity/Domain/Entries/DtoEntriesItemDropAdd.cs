namespace Master.Entity.Domain.Entries
{
    public class DtoEntriesItemDropAdd
    {
        public long fkFolder { get; set; }
        public long fkItem { get; set; }
        public long cents { get; set; }
        public long installments { get; set; }
        public long day { get; set; }
        public long month { get; set; }
        public long year { get; set; }
    }
}
