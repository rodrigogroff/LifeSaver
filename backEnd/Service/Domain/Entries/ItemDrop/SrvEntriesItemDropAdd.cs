using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Entries.ItemDrop
{
    public class SrvEntriesItemDropAdd : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();
        public IItemRepo itemRepo = new ItemRepo();
        public IItemDropRepo itemDropRepo = new ItemDropRepo();
        public IItemDropRegistryRepo itemDropRegistryRepo = new ItemDropRegistryRepo();

        public bool Add(string conn,
                                long fkUser,
                                long fkFolder,
                                long fkItem,
                                long cents,
                                long installments,
                                long day,
                                long month,
                                long year )
        {
            User user;
            Item item;
            ItemFolder folder;            

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemFolderRepo.GetById(conn, fkFolder, out folder))
                return ReportError("Folder information failed");

            if (folder.fkUser != user.id)
                return ReportError("Folder information failed");

            if (!itemRepo.GetById(conn, fkItem, out item))
                return ReportError("Item information failed");

            if (item.fkFolder != folder.id)
                return ReportError("Item information failed");

            if (item.fkUser != user.id)
                return ReportError("Item information failed");

            var newItem = new Entity.Database.ItemDrop
            {
                dtRegister = DateTime.Now,
                fkFolder = fkFolder,
                fkUser = fkUser,
                bActive = true,
                fkItem = fkItem,                
                nuInstallments = installments,
                nuDay = day,
                nuMonth  = month,
                nuYear = year,
                vlCents = cents
            };

            newItem.id = itemDropRepo.Insert(conn, newItem);

            if (newItem.id == 0)
                return ReportError("Item Drop information failed");

            var dtTarget = DateTime.Now;

            for (int i = 0; i < installments; i++)
            {
                var newRegistry = new ItemDropRegistry
                {
                    fkFolder = fkFolder,
                    fkItem = fkItem,
                    fkItemDrop = newItem.id,
                    fkUser = fkUser,
                    nuMonth = dtTarget.Month,
                    nuYear = dtTarget.Year,
                    nuPayment = i + 1,
                    vlCents = cents / installments
                };

                newRegistry.id = itemDropRegistryRepo.Insert(conn, newRegistry);

                dtTarget = dtTarget.AddMonths(1);
            }

            return true;
        }
    }
}
