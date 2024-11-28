
using Awards.Domain.Entities;
using Awards.Service.Interface;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace Awards.Api.Controller
{ 
    [ApiController]
    [Route("api/v{version:apiVersion}/awards")]
    public class AwardsController : ControllerBase
    {
        public IAwardsService _awardsService { get; }
        public AwardsController(IAwardsService awardsService){
            this._awardsService = awardsService;

        }
        [HttpGet]
        public async Task<IActionResult> GetMinMax()
        {
            return Ok(await this._awardsService.GetIntervals(2));
        }

        

        
         
    }
}
