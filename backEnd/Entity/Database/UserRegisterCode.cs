using System;

namespace Master.Entity.Database
{
    public class UserRegisterCode
    {
        public long id { get; set; }
        public long fkUser { get; set; }
        public string stCode { get; set; }
        public string stMobile { get; set; }
        public DateTime? dtRequest { get; set; }
        public DateTime? dtExpires { get; set; }
    }
}
