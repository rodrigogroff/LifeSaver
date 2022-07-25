
namespace Master.Entity.Domain.Config.Item
{
    public class DtoConfigItemEdit
    {
        public long? id { get; set; }
        public string new_name { get; set; }
        public long new_timePeriod { get; set; }
        public long new_standardValue { get; set; }
    }
}
