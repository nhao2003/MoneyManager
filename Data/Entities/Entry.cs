using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.Data.Entities;

[Table("Entries")]
public class Entry
{
    [Key] public int Id { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public DateTime Date { get; set; }

    public double Amount { get; set; }

    public bool IsIncome { get; set; }
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(ImagePath)}: {ImagePath}, {nameof(Date)}: {Date}, {nameof(Amount)}: {Amount}, {nameof(IsIncome)}: {IsIncome}, {nameof(CategoryId)}: {CategoryId} {nameof(Category)}: {Category}";
    }
}