using System.ComponentModel.DataAnnotations;

public class InventoryItemModel
{
    [Key]
    public int InventoryItemId { get; set; }

    [Required]
    public string ItemName { get; set; } = string.Empty;

    public string? Category { get; set; } 

    [Required]
    public int Quantity { get; set; }

    public string Unit { get; set; } = "pcs"; 

    public int ReorderLevel { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.Now;
}
