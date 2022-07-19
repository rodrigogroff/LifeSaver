using Master.Entity.Domain;
using Master.Infra;
using Master.Service.Domain.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Master.Controllers
{
    public partial class CtrlItemFolder : MasterController
    {
        public CtrlItemFolder(IOptions<LocalNetwork> _network) : base(_network) { }

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
    }
}
