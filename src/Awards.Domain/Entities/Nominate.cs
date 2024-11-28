using System;
using System.ComponentModel.DataAnnotations.Schema;
using Awards.Domain.Abstract;

namespace Awards.Domain.Entities;

[Table("Items")]
public class Nominate: Entity
{
 
    public int Year { get; set; }
    public string Title { get; set; }
    public string Studios { get; set; }
    public string Producers { get; set; }
    public bool Winner { get; set; }
}
