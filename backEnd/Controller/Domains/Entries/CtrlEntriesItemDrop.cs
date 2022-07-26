using Master.Entity.Domain.Config.Item;
using Master.Entity.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class CtrlEntriesItemDrop : MasterController
    {
        public CtrlEntriesItemDrop(IOptions<LocalNetwork> _network) : base(_network) { }
        
        [HttpPost]
        [Route("api/v1/entries/itemdrop_add")]
        public ActionResult itemdrop_add([FromBody] DtoConfigItemAdd obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();
            
            /*
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
            */

            #endregion

            return Ok();
        }
    }
}
