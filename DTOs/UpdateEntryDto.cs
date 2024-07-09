using MoneyManager.Data.Entities;

namespace MoneyManager.DTOs;

public class UpdateEntryDto
{
    public string Description { get; set; }

    public Category Category { get; set; }

    public string ImagePath { get; set; }

    public DateTime Date { get; set; }

    public double Amount { get; set; }

    public bool IsIncome { get; set; }
}