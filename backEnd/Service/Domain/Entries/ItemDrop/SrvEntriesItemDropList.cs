using System;
using System.Collections.Generic;
using Master.Entity.Database;
using Master.Entity.Domain.Entries;
using Master.Repository;

namespace Master.Service.Domain.Entries.ItemDrop
{
    public class SrvEntriesItemDropList : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();
        public IItemRepo itemRepo = new ItemRepo();
        public IItemDropRepo itemDropRepo = new ItemDropRepo();
        
        public bool List ( string conn,
                            long fkUser, 
                            long fkFolder, 
                            long? fkItem, 
                            long? day, 
                            long? month, 
                            long? year, 
                            out List<DtoEntriesItemDropListItem> lst )
        {
            User user;
            ItemFolder folder;
            Item item;

            lst = new List<DtoEntriesItemDropListItem>();

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

            if (fkItem != null)
            {
                if (!itemRepo.GetById(conn, (long)fkItem, out item))
                    return ReportError("Item information failed");

                if (item.fkUser != fkUser)
                    return ReportError("Item information failed");

                if (item.fkFolder != fkFolder)
                    return ReportError("Item information failed");
            }

            if (year == null)
                return ReportError("Year information failed");

            if (month == null)
                return ReportError("Month information failed");

            try
            {
                var _x = new DateTime((int)year, (int)month, (int)day);
            }
            catch
            {
                return ReportError("Date information failed");
            }

            List<Entity.Database.ItemDrop> lst_db;

            if (!itemDropRepo.GetByFkFolderFkItem(conn,
                                                    fkFolder,
                                                    fkItem,
                                                    day,
                                                    month,
                                                    year,
                                                    out lst_db))
            {
                return ReportError("ItemDrop information failed");
            }

            foreach (var i in lst_db)
            {
                lst.Add(new DtoEntriesItemDropListItem
                {
                    id = i.id,
                    cents = (long) i.vlCents,
                    date_register = D(i.dtRegister),
                    installments = (long) i.nuInstallments,
                    bIncome = (bool) i.bIncome,
                });
            }

            return true;
        }
    }
}
