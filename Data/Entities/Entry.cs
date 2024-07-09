using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.Data.Entities;

[Table("Entries")]
public class Entry
{
    [Key] public int Id { get; set; }

    public string Description { get; set; }

    public Category Category { get; set; }

    public string ImagePath { get; set; }

    public DateTime Date { get; set; }

    public double Amount { get; set; }

    public bool IsIncome { get; set; }
}