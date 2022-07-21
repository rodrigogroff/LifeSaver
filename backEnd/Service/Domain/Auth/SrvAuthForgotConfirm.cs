using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthForgotConfirm : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IUserPassRenewalRepo userPassRenewallRepo = new UserPassRenewalRepo();

        public bool Confirm(string conn, string mobile, string codigo, string password)
        {
            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(mobile))
                return ReportError("Mobile information failed");

            if (string.IsNullOrEmpty(codigo))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(password))
                return ReportError("Connection information failed");

            if (!IsNumber(mobile))
                return ReportError("Mobile information failed");

            if (mobile.Length < 11)
                return ReportError("Mobile information failed");

            if (!IsNumber(codigo))
                return ReportError("Token or code information failed");

            if (codigo.Length != 6)
                return ReportError("Token or code information failed");

            User mdl_user;
            UserPassRenewal mdl_code;

            if (!userRepo.GetUserByMobile(conn, mobile, out mdl_user))
                return ReportError("Error 0xE1");

            if (!userPassRenewallRepo.GetByFkUser(conn, mdl_user.id, out mdl_code))
                return ReportError("Error 0xE2");

            if (mdl_code == null)
                return ReportError("Error 0xE3");

            if (mdl_code.stCode != codigo)
                return ReportError("Error 0xE4");

            if (DateTime.Now > mdl_code.dtExpires)
                return ReportError("Error 0xE5");

            mdl_code.stCode = "";
            mdl_code.dtExpires = DateTime.Now;

            if (!userPassRenewallRepo.Update(conn, mdl_code))
                return ReportError("Error 0xE6");
                        
            mdl_user.stPassword = password;

            userRepo.Update(conn, mdl_user);

            return true;
        }
    }
}
