using Master.Entity.Domain;
using Master.Infra;
using Master.Service.Domain.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class CtrlConfigFolder : MasterController
    {
        public CtrlConfigFolder(IOptions<LocalNetwork> _network) : base(_network) { }
                
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

            return Ok();

            #endregion
        }
        
        [HttpPost]
        [Route("api/v1/config/folder_list")]
        public ActionResult folder_list([FromBody] DtoConfigFolderList obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvConfigFolderList();

            DtoConfigFolderListRet ret;

            if (!srv.FolderList(network.pgConnection, 
                                currentUser.ID(), 
                                obj.fkFolder, 
                                out ret ))
            {
                return BadRequest(srv.Error);
            }

            return Ok(new
            {
                result = ret.subfolders
            });

            #endregion
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

            return Ok();

            #endregion
        }
    }
}
