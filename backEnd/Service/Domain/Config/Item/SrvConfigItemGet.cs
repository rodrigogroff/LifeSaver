using Master.Entity.Database;
using Master.Entity.Domain.Config.Item;
using Master.Infra.Constant;
using Master.Repository;

namespace Master.Service.Domain.Config.Item
{
    public class SrvConfigItemGet : SrvBaseService
    {
        public IUserRepo userRepo = new UserRepo();
        public IItemRepo itemRepo = new ItemRepo();

        public bool ItemGet(string conn, long fkUser, long id, out DtoConfigItem ret)
        {
            ret = new DtoConfigItem();

            User user;
            Entity.Database.Item item;
            
            if (string.IsNullOrEmpty(conn))
                return ReportError("Connection information failed");

            if (fkUser <= 0)
                return ReportError("User information failed");

            if (!userRepo.GetUserById(conn, fkUser, out user))
                return ReportError("User information failed");

            if (!itemRepo.GetById(conn, id, out item))
                return false;

            if (item.fkUser != fkUser)
                return ReportError("User information failed");

            ret.id = id;
            ret.name = item.stName;
            ret.fkFolder = (long)item.fkFolder;
            ret.timePeriod = (long)item.nuPeriod;
            ret.standardValue = (long) item.vlBaseCents;
            ret.timePeriodDesc = TimePeriod.translate(item.nuPeriod);

            return true;
        }
    }
}
