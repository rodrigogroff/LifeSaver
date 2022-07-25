using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Config.Folder
{
    public class SrvConfigFolderAdd : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();

        public bool FolderAdd(string conn,
                                long fkUser,
                                long? fkFolder,
                                string name,
                                bool income)
        {
            User user;
            ItemFolder tst;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(name))
                return ReportError("Folder information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (string.IsNullOrEmpty(name))
                return ReportError("Name information failed");

            if (fkFolder != null)
            {
                ItemFolder parentFolder = null;

                if (!itemFolderRepo.GetById(conn, (long)fkFolder, out parentFolder))
                    return ReportError("Folder information failed");

                if (parentFolder.fkUser != user.id)
                    return ReportError("Folder information failed");
            }

            itemFolderRepo.GetByUserFolderName(conn, fkUser, fkFolder, name, out tst);

            if (tst != null)
                return ReportError("Folder already exists");

            var newFolder = new ItemFolder
            {
                bIncome = income,
                dtRegister = DateTime.Now,
                fkFolder = fkFolder,
                fkUser = fkUser,
                stName = name
            };

            newFolder.id = itemFolderRepo.Insert(conn, newFolder);

            if (newFolder.id == 0)
                return ReportError("Folder information failed");

            return true;
        }
    }
}
