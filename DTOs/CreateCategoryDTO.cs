namespace MoneyManager.DTOs;

public class CreateCategoryDto
{
    public required string Name { get; set;}
    
    public required string Icon { get; set; }
    
    public required bool IsIncome { get; set; }
}