using System;
using System.Reflection.Metadata.Ecma335;

namespace AwardsService.DTOs;
 
public class WinnerYearDTO
{     
    public string Producer { get; set; }

    public int interval { get { return followingWin - previousWin; } }

    public int previousWin { get; set; } 

     
    public int followingWin { get; set; } 
}