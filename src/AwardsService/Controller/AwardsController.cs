using AwardsService.Data;
using AwardsService.DTOs;
using AwardsService.Entities;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace AwardsService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly AwardsDbContext _context;

        public AwardsController(AwardsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMinMax()
        {

            var winners = await _context.Nominates.Where(c => c.Winner).ToListAsync();
            var winnersByYear = new List<WinnerYearDTO>();
            var dictWinnerYear = new Dictionary<string, List<int>>();
            var dictIntervalWinner = new Dictionary<int, List<WinnerYearDTO>>();

            foreach (var win in winners)
            {

                if (dictWinnerYear.ContainsKey(win.Producers))
                    dictWinnerYear[win.Producers].Add(win.Year);
                else
                    dictWinnerYear.Add(win.Producers, new List<int>() { win.Year });

            }

            foreach (var producer in dictWinnerYear.Where(c =>c.Value.Count > 1))
            { 
                var years = producer.Value; 
                
                for (int i = 1; i < years.Count; i++)
                { 
                    int range = years[i] - years[i - 1];

                    var winnerDtoMin = new WinnerYearDTO()
                    {
                        Producer = producer.Key,
                        previousWin = years[i - 1],
                        followingWin = years[i]
                    };
 
                    if (dictIntervalWinner.ContainsKey(range))
                        dictIntervalWinner[range].Add(winnerDtoMin);
                    else
                        dictIntervalWinner.Add(range, new List<WinnerYearDTO>() { winnerDtoMin });
                }
 
            }

            var result = new
            {
                min = dictIntervalWinner.Count() > 1 ? dictIntervalWinner[dictIntervalWinner.Keys.Min()].Take(2) : [],
                max = dictIntervalWinner.Count() > 1 ? dictIntervalWinner[dictIntervalWinner.Keys.Max()].Take(2) : []
            };

            return Ok(result);
        }
    }
}
