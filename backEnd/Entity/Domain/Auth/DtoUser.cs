using System;

namespace Master.Entity.Domain
{
    public class DtoUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string dt_last_login { get; set; }
        public string dt_join { get; set; }

        public long ID()
        {
            return Convert.ToInt64(id);
        }
    }
}
