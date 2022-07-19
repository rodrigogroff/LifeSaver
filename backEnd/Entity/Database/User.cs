using System;

namespace Master.Entity.Database
{
    public class User
    {
        public long id { get; set; }

        public string stName { get; set; }
        public string stEmail { get; set; }
        public string stMobile { get; set; }
        public string stPassword { get; set; }
        
        public bool? bActive { get; set; }
        
        public DateTime? dtJoin { get; set; }
        public DateTime? dtLastLogin { get; set; }
    }
}
