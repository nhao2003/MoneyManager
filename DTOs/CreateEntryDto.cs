using MoneyManager.Data.Entities;

namespace MoneyManager.DTOs;

public class CreateEntryDto
{
    public required string Description { get; set; }

    public required Category Category { get; set; }

    public required string ImagePath { get; set; }

    public required DateTime Date { get; set; }

    public required double Amount { get; set; }

    public required bool IsIncome { get; set; }
}