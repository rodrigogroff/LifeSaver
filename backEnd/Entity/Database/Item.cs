using System;

namespace Master.Entity.Database
{
    public class Item
    {
        public long id { get; set; }
        public long? fkUser { get; set; }
        public long? fkFolder { get; set; }
        public long? nuPeriod { get; set; }
        public long? nuExpectedDay { get; set; }
        public long? vlBaseCents { get; set; }
        public bool? bIncome { get; set; }
        public string stName { get; set; }
        public DateTime? dtRegister { get; set; }
    }
}
