using System;

namespace Master.Entity.Database
{
    public class ItemDrop
    {
        public long id { get; set; }
        public long? fkItem { get; set; }
        public long? fkUser { get; set; }
        public long? fkFolder { get; set; }
        public long? vlCents { get; set; }
        public long? nuDay { get; set; }
        public long? nuMonth { get; set; }
        public long? nuYear { get; set; }
        public long? nuInstallments { get; set; }
        public bool? bActive { get; set; }
        public DateTime? dtRegister { get; set; }
    }
}
