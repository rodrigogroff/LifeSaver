using Master.Entity.Database;
using Master.Repository;

namespace Master.Service.Domain.Config.Folder
{
    public class SrvConfigFolderEdit : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();

        public bool FolderEdit(string conn,
                                long fkUser,
                                long fkFolder,
                                string new_name)
        {
            User user;
            ItemFolder editFolder, tst;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(new_name))
                return ReportError("Folder information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemFolderRepo.GetById(conn, fkFolder, out editFolder))
                return ReportError("Folder information failed");

            if (editFolder.fkUser != user.id)
                return ReportError("Folder information failed");

            itemFolderRepo.GetByUserFolderName(conn, fkUser, editFolder.fkFolder, new_name, out tst);

            if (tst != null)
                return ReportError("Folder already exists");

            editFolder.stName = new_name;

            if (!itemFolderRepo.Update(conn, editFolder))
                return ReportError("EX01");

            return true;
        }
    }
}
