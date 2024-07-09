using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Data.Entities;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }
    
    public string Icon { get; set; }
    
    public string Name { get; set; }
    
    public bool IsIncome { get; set; }
}