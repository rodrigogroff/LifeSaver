using System;

namespace Master.Entity.Database
{
    public class ItemFolder
    {
        public long id { get; set; }
        public long? fkFolder { get; set; }
        public long? fkUser { get; set; }
        public DateTime? dtRegister { get; set; }
        public bool? bIncome { get; set; }
        public string stName { get; set; }
    }
}
