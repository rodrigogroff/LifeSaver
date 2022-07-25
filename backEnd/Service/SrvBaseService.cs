using Master.Entity.Infra;
using System;

namespace Master.Service
{
    public class SrvBaseService 
    {
        public string sgbdConnectionString { get; set; }
        public DtoServiceError Error { get; set; }

        public bool IsNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
                if (!Char.IsNumber(str[i]))
                    return false;

            return true;
        }

        public bool ReportError(string msg)
        {
            Error = new DtoServiceError { message = msg };
            return false;
        }

        public string D(DateTime? dt)
        {
            return Convert.ToDateTime(dt).ToString("dd/MM/yyyy HH:mm");
        }
    }
}
