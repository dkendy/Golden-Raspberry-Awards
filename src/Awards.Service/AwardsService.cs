

using Awards.Domain.Entities;
using Awards.Service.Interface;
using Awards.Service.Response; 

namespace Awards.Service;

public class AwardsService: IAwardsService
{
    private readonly INominateRepository _repository;

    public AwardsService(INominateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Intervals> GetIntervals(int take)
    {
        var winners = await _repository.GetAll();
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

        foreach (var producer in dictWinnerYear.Where(c => c.Value.Count > 1))
        {
            var years = producer.Value.OrderBy(x=>x).ToList();

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

        var result = new Intervals(){
            Min = dictIntervalWinner.Count() > 1 ? dictIntervalWinner[dictIntervalWinner.Keys.Min()].Take(take).ToList() : [],
            Max = dictIntervalWinner.Count() > 1 ? dictIntervalWinner[dictIntervalWinner.Keys.Max()].Take(take).ToList() : []
        };

        return result; 
    }

    public Task<Guid> Insert(Nominate request)
    {
        var issertDbId = _repository.Insert(request);

        return issertDbId;
    }

    public async Task<int> Count()
    { 
        var queryCount =  await _repository.GetAll();
        return queryCount.Count();
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);

    }
}
