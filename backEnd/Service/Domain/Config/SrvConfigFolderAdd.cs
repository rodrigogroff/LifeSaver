using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Config
{
    public class SrvConfigFolderAdd : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();

        public bool FolderAdd ( string conn, 
                                long fkUser, 
                                long? fkFolder, 
                                string name, 
                                bool income)
        {
            User user;
            ItemFolder parentFolder = null;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (string.IsNullOrEmpty(name))
                return ReportError("Name information failed");

            if (fkFolder != null)
            {
                if (!itemFolderRepo.GetById(conn, (long)fkFolder, out parentFolder))
                    return ReportError("Folder information failed");

                if (parentFolder.fkUser != user.id)
                    return ReportError("Folder information failed");
            }

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
