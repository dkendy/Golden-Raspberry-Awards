using System;
using System.Reflection.Metadata.Ecma335;

namespace Awards.Service.Response;
 
public class WinnerYearDTO
{     
    public string Producer { get; set; }

    public int interval { get { return followingWin - previousWin; } }

    public int previousWin { get; set; } 

     
    public int followingWin { get; set; } 
}