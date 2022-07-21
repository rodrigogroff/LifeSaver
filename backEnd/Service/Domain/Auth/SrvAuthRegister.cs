using Master.Entity.Database;
using Master.Entity.Domain;
using Master.Repository;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthMagicSmsList : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IUserRegisterCodeRepo userRegisterCodeRepo = new UserRegisterCodeRepo();

        public bool List(string conn, string mobile, out DtoAuthSmsListRet ret)
        {
            ret = new DtoAuthSmsListRet();

            User user;

            if (!userRepo.GetUserByMobile(conn, mobile, out user))
                return false;

            UserRegisterCode retCode;

            if (!userRegisterCodeRepo.GetByFkUser(conn, user.id, out retCode ))
                return false;

            if (retCode == null)
                return false;

            ret.codes.Add(retCode.stCode);

            return true;
        }
    }
}
