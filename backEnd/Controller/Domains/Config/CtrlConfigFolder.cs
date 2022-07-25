using Master.Entity.Domain.Config.Folder;
using Master.Entity.Infra;
using Master.Service.Domain.Config.Folder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class CtrlConfigFolder : MasterController
    {
        public CtrlConfigFolder(IOptions<LocalNetwork> _network) : base(_network) { }

        [NonAction]
        public void CacheCleanup(long userId, long? fkFolder)
        {
            var srvCache = new SrvConfigFolderList();
            var tag = srvCache.cacheTag(userId, fkFolder);

            DeleteCacheFile(tag);
        }

        [HttpPost]
        [Route("api/v1/config/folder")]
        public ActionResult folder([FromBody] DtoConfigFolderGet obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigFolderAdd();

            DtoConfigFolder ret;

            if (!srv.FolderAdd(network.pgConnection,
                                currentUser.ID(),
                                obj.id,
                                out ret))
            {
                return BadRequest(srv.Error);
            }

            #endregion

            return Ok(new 
            {
                result = ret
            });
        }

        [HttpPost]
        [Route("api/v1/config/folder_add")]
        public ActionResult folder_add([FromBody] DtoConfigFolderAdd obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigFolderAdd();

            if (!srv.FolderAdd(network.pgConnection,
                                currentUser.ID(),
                                obj.fkFolder,
                                obj.name,
                                obj.income))
            {
                return BadRequest(srv.Error);
            }

            #endregion

            CacheCleanup(currentUser.ID(), obj.fkFolder);

            return Ok();
        }
        
        [HttpPost]
        [Route("api/v1/config/folder_edit")]
        public ActionResult folder_edit([FromBody] DtoConfigFolderEdit obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigFolderEdit();

            if (!srv.FolderEdit(network.pgConnection,
                                currentUser.ID(),
                                obj.id,
                                obj.new_name))
            {
                return BadRequest(srv.Error);
            }

            #endregion

            // subfolder needs to clean cache
            CacheCleanup(currentUser.ID(), srv.edit_fkFolder);

            return Ok();
        }

        [HttpPost]
        [Route("api/v1/config/folder_list")]
        public ActionResult folder_list([FromBody] DtoConfigFolderList obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigFolderList();

            DtoConfigFolderListRet ret;

            var fileName = srv.cacheTag(currentUser.ID(), obj.fkFolder);
            var cache = GetCacheFile(fileName);

            if (cache != null)
            {
                ret = JsonSerializer.Deserialize<DtoConfigFolderListRet>(cache);
            }
            else
            {
                if (!srv.FolderList(network.pgConnection,
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
