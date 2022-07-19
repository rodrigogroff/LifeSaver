using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthRegisterConfirm : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IUserRegisterCodeRepo userRegisterCodeRepo = new UserRegisterCodeRepo();

        public bool Confirm(string conn, string mobile, string codigo)
        {
            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");
            
            if (string.IsNullOrEmpty(mobile))
                return ReportError("Mobile information failed");

            if (mobile.Length < 11)
                return ReportError("Mobile information failed");

            if (string.IsNullOrEmpty(codigo))
                return ReportError("Token or code information failed");

            if (!IsNumber(mobile))
                return ReportError("Mobile information failed");

            if (!IsNumber(codigo))
                return ReportError("Token or code information failed");

            User mdl_user;
            UserRegisterCode mdl_code;

            if (!userRepo.GetUserByMobile(conn, mobile, out mdl_user))
                return ReportError("Error 0xE1");
            
            if (!userRegisterCodeRepo.GetByFkUser(conn, mdl_user.id, out mdl_code))
                return ReportError("Error 0xE2");

            if (mdl_code == null)
                return ReportError("Error 0xE3");

            if (mdl_code.stCode != codigo)
                return ReportError("Error 0xE4");

            if (DateTime.Now > mdl_code.dtExpires)
                return ReportError("Error 0xE5");

            mdl_code.stCode = "";
            mdl_code.dtExpires = DateTime.Now;

            if (!userRegisterCodeRepo.Update(conn, mdl_code))
                return ReportError("Error 0xE6");

            return true;
        }
    }
}
