
namespace Master.Entity.Domain.Config.Item
{
    public class DtoConfigItemAdd
    {
        public long fkFolder { get; set; }
        public string name { get; set; }
        public long timePeriod { get; set; }
        public long standardValue { get; set; }
    }
}
