using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthGenerateCode : SrvBaseService
    {
        public string Keygen()
        {
            Random rd = new();
            int rand_num = rd.Next(100000, 999999);
            return rand_num.ToString();
        }
    }
}
