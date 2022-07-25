using Master.Entity.Domain.Config.Item;
using Master.Entity.Infra;
using Master.Service.Domain.Config.Folder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class CtrlConfigItem : MasterController
    {
        public CtrlConfigItem(IOptions<LocalNetwork> _network) : base(_network) { }

        [NonAction]
        public void CacheCleanup(long userId, long? fkFolder)
        {
            var srvCache = new SrvConfigItemList();
            var tag = srvCache.cacheTag(userId, fkFolder);

            DeleteCacheFile(tag);
        }

        [HttpPost]
        [Route("api/v1/config/item_add")]
        public ActionResult item_add([FromBody] DtoConfigItemAdd obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();
            
            var srv = new SrvConfigItemAdd();

            ///TODO
            if (!srv.ItemAdd(network.pgConnection,
                                currentUser.ID(),
                                obj.fkFolder,
                                obj.name,
                                obj.timePeriod,
                                obj.standardValue))
            {
                return BadRequest(srv.Error);
            }

            #endregion

            CacheCleanup(currentUser.ID(), obj.fkFolder);

            return Ok();
        }
        
        [HttpPost]
        [Route("api/v1/config/item_list")]
        public ActionResult item_list([FromBody] DtoConfigItemList obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();
            
            var srv = new SrvConfigItemList();

            DtoConfigItemListRet ret;

            var fileName = srv.cacheTag(currentUser.ID(), obj.fkFolder);
            var cache = GetCacheFile(fileName);

            if (cache != null)
            {
                ret = JsonSerializer.Deserialize<DtoConfigItemListRet>(cache);
            }
            else
            {
                if (!srv.ItemList(network.pgConnection,
                                currentUser.ID(),
                                obj.fkFolder,
                                out ret))
                {
                    return BadRequest(srv.Error);
                }

                SaveCacheFile(fileName, JsonSerializer.Serialize(ret));
            }

            return Ok(new
            {
                results = ret.list
            });

            #endregion
        }
    }
}
