using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awards.Domain.Entities;
using Awards.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Awards.Api.Controller
{
    [ApiController]
    [Route("api/v{version:apiVersion}/data")] 
    public class DataController: ControllerBase
    {
        public IAwardsService _awardsService { get; }
        public DataController(IAwardsService awardsService){
            this._awardsService = awardsService;

        }
        [HttpGet]
        public async Task<IActionResult> Total()
        {
            return Ok(await this._awardsService.Count());
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            this._awardsService.Delete(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Nominate request)
        {
            var resultInsert = await _awardsService.Insert(request);

            if(resultInsert == new Guid()) 
                return BadRequest();
            else
                return  CreatedAtAction(nameof(Insert), resultInsert );
        }
    }
}