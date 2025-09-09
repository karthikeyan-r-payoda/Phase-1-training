public class AddInventoryItemDto
{
    public string ItemName { get; set; }
    public string? Category { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public int ReorderLevel { get; set; }
}
