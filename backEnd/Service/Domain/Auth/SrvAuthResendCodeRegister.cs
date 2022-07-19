using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthResendCodeRegister : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IUserPassRenewalRepo userPassRenewallRepo = new UserPassRenewalRepo();

        public bool ResendCodeMobile(string conn, string mobile, int expMinutes)
        {
            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");
            
            if (string.IsNullOrEmpty(mobile))
                return ReportError("Mobile information failed");

            if (!IsNumber(mobile))
                return ReportError("Mobile information failed");

            if (mobile.Length < 11)
                return ReportError("Mobile information failed");

            User mdl_user;

            if (!userRepo.GetUserByMobile(conn, mobile, out mdl_user))
                return ReportError("Error 0xE1");

            var mdl_code = new UserPassRenewal
            {
                dtRequest = DateTime.Now,
                dtExpires = DateTime.Now.AddMinutes(expMinutes),
                fkUser = mdl_user.id,
                stCode = new SrvAuthGenerateCode().Keygen(),
                stMobile = mobile,
            };

            mdl_code.id = userPassRenewallRepo.Insert(conn, mdl_code);

            if (mdl_code.id == 0)
                return ReportError("Error 0xE2");

            return true;
        }
    }
}
