using Master.Entity.Domain.Config.Item;
using Master.Entity.Infra;
using Master.Service.Domain.Config.Item;
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
        [Route("api/v1/config/item_get")]
        public ActionResult item_get([FromBody] DtoConfigItemGet obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigItemGet();

            DtoConfigItem ret;

            if (!srv.ItemGet(network.pgConnection,
                            currentUser.ID(),
                            obj.id,
                            out ret))
            {
                return BadRequest(srv.Error);
            }
            
            return Ok(new
            {
                result = ret
            });

            #endregion
        }

        [HttpPost]
        [Route("api/v1/config/item_add")]
        public ActionResult item_add([FromBody] DtoConfigItemAdd obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();
            
            var srv = new SrvConfigItemAdd();
            
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

            if (network.cache)
                CacheCleanup(currentUser.ID(), obj.fkFolder);

            return Ok();
        }

        [HttpPost]
        [Route("api/v1/config/item_edit")]
        public ActionResult item_edit([FromBody] DtoConfigItemEdit obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigItemEdit();
            
            if (!srv.ItemEdit(network.pgConnection,
                                currentUser.ID(),
                                obj.id,
                                obj.new_name ))
            {
                return BadRequest(srv.Error);
            }

            #endregion

            if (network.cache)
                CacheCleanup(currentUser.ID(), obj.id);

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

            if (network.cache)
            {
                var fileName = srv.cacheTag(currentUser.ID(), obj.fkFolder);
                var cache = GetCacheFile(fileName);

                if (cache != null)
                {
                    ret = JsonSerializer.Deserialize<DtoConfigItemListRet>(cache);
                }
            }
            
            if (!srv.ItemList(network.pgConnection,
                            currentUser.ID(),
                            obj.fkFolder,
                            out ret))
            {
                return BadRequest(srv.Error);
            }

            if (network.cache)
            {
                var fileName = srv.cacheTag(currentUser.ID(), obj.fkFolder);
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
