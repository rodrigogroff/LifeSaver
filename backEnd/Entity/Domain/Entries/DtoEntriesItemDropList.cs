namespace Master.Entity.Domain.Entries
{
    public class DtoEntriesItemDropList
    {
        public long fkFolder { get; set; }
        public long? fkItem { get; set; }
        public long? day { get; set; }
        public long? month { get; set; }
        public long? year { get; set; }
    }
}
