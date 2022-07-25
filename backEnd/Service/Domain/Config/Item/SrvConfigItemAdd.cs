using Master.Entity.Database;
using Master.Infra;
using Master.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Service.Domain.Config.Item
{
    public class SrvConfigItemAdd : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();
        public IItemRepo itemRepo = new ItemRepo();

        public bool ItemAdd(string conn,
                                long fkUser,
                                long fkFolder,
                                string name,
                                long timePeriod,
                                long standardValue)
        {
            User user;
            ItemFolder folder;
            Master.Entity.Database.Item tst;
            List<Master.Entity.Database.Item> list;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (string.IsNullOrEmpty(name))
                return ReportError("Item information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemFolderRepo.GetById(conn, fkFolder, out folder))
                return ReportError("Folder information failed");

            if (!TimePeriod.Check((int)timePeriod))
                return ReportError("Time period failed");

            itemRepo.GetListByFkUserFkFolder(conn, fkUser, fkFolder, out list);

            if (list.Any (y=>y.stName.ToLower() == name.ToLower()))
                return ReportError("Item already exists");

            var newItem = new Master.Entity.Database.Item
            {
                bIncome = true,
                nuPeriod = timePeriod,
                vlBaseCents = standardValue,
                dtRegister = DateTime.Now,
                fkFolder = fkFolder,
                fkUser = fkUser,
                stName = name
            };

            newItem.id = itemRepo.Insert(conn, newItem);

            if (newItem.id == 0)
                return ReportError("Folder information failed");

            return true;
        }
    }
}
