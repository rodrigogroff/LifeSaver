using Master.Entity.Database;
using Master.Entity.Domain;
using Master.Repository;
using System.Collections.Generic;

namespace Master.Service.Domain.Config
{
    public class SrvConfigFolderList : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();

        public bool FolderList ( string conn, long fkUser, long? fkFolder, out DtoConfigFolderListRet ret)
        {
            ret = new DtoConfigFolderListRet();

            User user;
            List<ItemFolder> lst;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemFolderRepo.GetListByFkUser(conn, fkUser, fkFolder, out lst))
                return false;

            foreach (var item in lst)
            {
                ret.subfolders.Add(new DtoConfigFolderDetail
                {
                    id = item.id,
                    name = item.stName,
                });
            }

            return true;
        }
    }
}
