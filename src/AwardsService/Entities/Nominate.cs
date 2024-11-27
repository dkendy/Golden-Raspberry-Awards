using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwardsService.Entities;

[Table("Items")]
public class Nominate
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Title { get; set; }
    public string Studios { get; set; }
    public string Producers { get; set; }
    public bool Winner { get; set; }
}
