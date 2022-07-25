using Master.Entity.Database;
using Master.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Master.Service.Domain.Config.Item
{
    public class SrvConfigItemEdit : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();
        public IItemRepo itemRepo = new ItemRepo();

        public bool ItemEdit(string conn,
                                long fkUser,
                                long id,
                                string new_name)
        {
            User user;
            Master.Entity.Database.Item edit;
            List<Master.Entity.Database.Item> list;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(new_name))
                return ReportError("Folder information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            itemRepo.GetById(conn, id, out edit);

            if (edit.fkUser != fkUser)
                return ReportError("User information failed");

            itemRepo.GetListByFkUserFkFolder(conn, fkUser, (long)edit.fkFolder, out list);

            if (list.Any(y => y.stName.ToLower() == new_name.ToLower()))
                return ReportError("Item already exists");

            edit.stName = new_name;

            if (!itemRepo.Update(conn, edit))
                return ReportError("EX01");

            return true;
        }
    }
}

