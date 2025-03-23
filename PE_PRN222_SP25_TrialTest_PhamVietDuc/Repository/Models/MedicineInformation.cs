using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models;

public partial class MedicineInformation
{
    [Required]
    public string MedicineId { get; set; } = null!;
    [Required]
    public string MedicineName { get; set; } = null!;
    [Required]
    [MinLength(10, ErrorMessage = "ActiveIngredients must be at least 10 characters long.")]
    [RegularExpression(@"^(?:[A-Z0-9][a-z0-9]*\s?)+$",
            ErrorMessage = "Each word must start with a capital letter or a number, and no special characters like #, @, &, (, ) are allowed.")]
    public string ActiveIngredients { get; set; } = null!;
    [Required]
    public string? ExpirationDate { get; set; }
    [Required]
    public string DosageForm { get; set; } = null!;
    [Required]
    public string WarningsAndPrecautions { get; set; } = null!;
    [Required]
    public string? ManufacturerId { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
}
