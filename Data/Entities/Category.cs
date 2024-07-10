using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.Data.Entities;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }
    
    public string Icon { get; set; }
    
    public string Name { get; set; }
    
    public bool IsIncome { get; set; }
    
    public ICollection<Entry> Entries { get; set; }
    
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Icon)}: {Icon}, {nameof(Name)}: {Name}, {nameof(IsIncome)}: {IsIncome}, {nameof(Entries)}: {Entries}";
    }
}