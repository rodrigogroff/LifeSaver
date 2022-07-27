namespace Master.Entity.Domain.Config.Item
{
    public class DtoConfigItem
    {
        public long id { get; set; }
        public long fkFolder { get; set; }
        public string name { get; set; }
        public long timePeriod { get; set; }
        public string timePeriodDesc { get; set; }
        public long standardValue { get; set; }
    }
}
