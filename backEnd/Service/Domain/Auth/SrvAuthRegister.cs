using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthRegister : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IUserRegisterCodeRepo userRegisterCodeRepo = new UserRegisterCodeRepo();

        public bool Register(string conn, string name, string email, string mobile, string password, int expMinutes)
        {
            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(name))
                return ReportError("Name information failed");

            if (string.IsNullOrEmpty(email))
                return ReportError("Email information failed");

            if (string.IsNullOrEmpty(mobile))
                return ReportError("Mobile information failed");

            if (mobile.Length < 11)
                return ReportError("Mobile information failed");

            if (string.IsNullOrEmpty(password))
                return ReportError("Password information failed");

            if (!IsNumber(mobile))
                return ReportError("Mobile information failed");

            if (password.Length < 6)
                return ReportError("Password information failed");

            User usr_test;

            if (!userRepo.GetUserByMobile(conn, mobile, out usr_test))
                return ReportError("Mobile already used");

            var mdl_user = new User
            {
                bActive = true,
                dtJoin = DateTime.Now,
                dtLastLogin = null,
                stEmail  = email,
                stName = name,
                stMobile = mobile,
                stPassword = password
            };

            mdl_user.id = userRepo.Insert(conn, mdl_user);

            if (mdl_user.id == 0)
                return ReportError("Error 0xE1");

            var mdl_code = new UserRegisterCode
            {
                dtRequest = DateTime.Now,
                dtExpires = DateTime.Now.AddMinutes(expMinutes),
                fkUser = mdl_user.id,
                stCode = new SrvAuthGenerateCode().Keygen(),
                stMobile = mobile,
            };

            mdl_code.id = userRegisterCodeRepo.Insert(conn, mdl_code);

            if (mdl_code.id == 0)
                return ReportError("Error 0xE2");

            return true;
        }
    }
}
