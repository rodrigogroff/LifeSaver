using Master.Entity.Database;
using Master.Entity.Domain.Config.Item;
using Master.Infra.Constant;
using Master.Repository;
using System.Collections.Generic;

namespace Master.Service.Domain.Config.Item
{
    public class SrvConfigItemList : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemRepo itemRepo = new ItemRepo();

        public string cacheTag(long fkUser, long? fkFolder)
        {
            return "ItemList" + 
                    fkUser.ToString().PadLeft(12, '0') +
                    (fkFolder == null ? "" : fkFolder.ToString()).PadLeft(12, '0');
        }

        public bool ItemList(string conn, long fkUser, long fkFolder, out DtoConfigItemListRet ret)
        {
            ret = new DtoConfigItemListRet();

            User user;
            List<Master.Entity.Database.Item> lst;

            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemRepo.GetListByFkUserFkFolder(conn, fkUser,fkFolder, out lst))
                return false;

            foreach (var item in lst)
            {
                ret.list.Add(new DtoConfigItem
                {
                    id = item.id,
                    fkFolder = (long) item.fkFolder,
                    name = item.stName,
                    standardValue = (long) item.vlBaseCents,
                    timePeriod = (long) item.nuPeriod,
                    timePeriodDesc = TimePeriod.translate(item.nuPeriod)
                });
            }

            return true;
        }
    }
}
