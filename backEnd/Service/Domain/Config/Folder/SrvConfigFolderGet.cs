using Master.Entity.Database;
using Master.Entity.Domain.Config.Folder;
using Master.Repository;

namespace Master.Service.Domain.Config.Folder
{
    public class SrvConfigFolderGet : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();
        
        public bool FolderGet(string conn, long fkUser, long id, out DtoConfigFolderGetRet ret)
        {
            ret = new DtoConfigFolderGetRet();

            User user;
            ItemFolder obj;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemFolderRepo.GetById(conn, id, out obj))
                return false;

            ret.id = id;
            ret.fkFolder = obj.fkFolder;
            ret.date = D(obj.dtRegister);
            ret.income = (bool) obj.bIncome;
            ret.name = obj.stName;

            return true;
        }
    }
}
