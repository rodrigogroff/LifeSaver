using Master.Entity.Database;
using Master.Repository;
using System;

namespace Master.Service.Domain.Config.Folder
{
    public class SrvConfigItemAdd : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemFolderRepo itemFolderRepo = new ItemFolderRepo();

        public bool ItemAdd(string conn,
                                long fkUser,
                                long? fkFolder,
                                string name,
                                long timePeriod,
                                long standardValue)
        {
            User user;
            
            return true;
        }
    }
}
