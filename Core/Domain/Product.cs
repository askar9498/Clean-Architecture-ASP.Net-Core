﻿using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    public int Id { get; private set; } = 0;

    public bool IsAvailable { get; private set; }

    [EmailAddress]
    public string ManufactureEmail { get; private set; }

    [Phone]
    public string ManufacturePhone { get; private set; }

    public DateTime ProduceDate { get; private set; }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    public Product(string name, bool isAvailable, string manufactureEmail, string manufacturePhone, DateTime produceDate)
    {
        Name = name;
        IsAvailable = isAvailable;
        ManufactureEmail = manufactureEmail;
        ManufacturePhone = manufacturePhone;
        ProduceDate = produceDate;
    }
}