using Master.Entity.Database;
using Master.Entity.Domain;
using Master.Repository;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthLogin : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();

        public bool Login(string conn, string email, string password, out DtoUser usr)
        {
            usr = new DtoUser();

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(email))
                return ReportError("Email information failed");

            if (string.IsNullOrEmpty(password))
                return ReportError("Password information failed");

            User db_user;

            if (!userRepo.GetUserByEmail(conn, email, out db_user))
                return ReportError("Error 0xE1");

            if (db_user.bActive == false)
                return ReportError("Error 0xE2");

            if (db_user.stPassword != password)
                return ReportError("Error 0xE3");

            usr.id = db_user.id.ToString();
            usr.name = db_user.stName;
            usr.email = db_user.stEmail;
            usr.mobile = db_user.stMobile;
            
            return true;
        }
    }
}
