using Master.Entity.Domain.Entries;
using Master.Entity.Infra;
using Master.Service.Domain.Entries.ItemDrop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class CtrlEntriesItemDrop : MasterController
    {
        public CtrlEntriesItemDrop(IOptions<LocalNetwork> _network) : base(_network) { }
        
        [HttpPost]
        [Route("api/v1/entries/itemdrop_add")]
        public ActionResult itemdrop_add([FromBody] DtoEntriesItemDropAdd obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();
            
            var srv = new SrvEntriesItemDropAdd();
            
            if (!srv.Add(network.pgConnection,
                            currentUser.ID(),
                            obj.fkFolder,
                            obj.fkItem,
                            obj.cents,
                            obj.installments,
                            obj.day,
                            obj.month,
                            obj.year ))
            {
                return BadRequest(srv.Error);
            }
            
            return Ok();

            #endregion
        }

        [HttpPost]
        [Route("api/v1/entries/itemdrop_list")]
        public ActionResult itemdrop_list([FromBody] DtoEntriesItemDropList obj)
        {
            #region - code - 

            var currentUser = GetCurrentAuthenticatedUser();

            var srv = new SrvEntriesItemDropList();

            List<DtoEntriesItemDropListItem> lst;

            if (!srv.List(network.pgConnection,
                            currentUser.ID(), 
                            obj.fkFolder,
                            obj.fkItem,
                            obj.day,
                            obj.month,
                            obj.year,
                            out lst ))
            {
                return BadRequest(srv.Error);
            }

            return Ok(new
            {
                results = lst,
                totalCents = lst.Sum ( y=> y.cents ),
            });

            #endregion
        }
    }
}
